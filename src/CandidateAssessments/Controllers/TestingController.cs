using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using CandidateAssessments.Models;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateAssessments.Controllers
{
    [AllowAnonymous]
    public class TestingController : Controller
    {
        private AssessmentContext _db;

        public TestingController(AssessmentContext context)
        {
            _db = context;
        }

        // GET: /testing/
        public IActionResult Index()
        {
            return View();
        } 
        
        // GET: /testing/assessment/{code}
        public IActionResult Assessment(string accessCode)
        {
            // if access code is not null, then save it as a cookie.
            if (!string.IsNullOrEmpty(accessCode))
                SetAccessCodeCookie(accessCode);
            else
                accessCode = GetAccessCodeCookie();

            if (string.IsNullOrEmpty(accessCode))
                return new HttpUnauthorizedResult();


            // retrieve accessment by access code alternate key (be sure to include the quiz list)
            Assessment assessment = _db.Assessments.Where(x => x.AccessCode == accessCode).Include(x => x.Quizes).ThenInclude(y => y.Topic).FirstOrDefault();
            

            if (assessment == null)
                return new HttpNotFoundResult();


            // validate that the access code has not expired
            if (assessment.ExpirationDate < DateTime.Now)
                return View("Expired");

                // return the assessment to the view
            return View(assessment);
        }

        // GET: /testing/quiz/id
        public IActionResult Quiz(int id)
        {
            // Get the assessment from the db based on access code cookie.
            string accessCode = GetAccessCodeCookie();

            if (string.IsNullOrEmpty(accessCode))
                return HttpUnauthorized();

            Assessment assessment = _db.Assessments.Where(x => x.AccessCode == accessCode).FirstOrDefault();

            if (assessment == null)
                return HttpNotFound();
            
            // Get the quiz from the db by id
            Quiz quiz = _db.Quizes.Where(x => x.QuizId == id).FirstOrDefault();


            // Validation
            // Quiz is a part of the assessment of the corresponding access code cookie
            if(quiz.AssessmentId != assessment.AssessmentId)
                return HttpUnauthorized();

            // Quiz is has been completed
            if (quiz.TimeCompleted != null)
                return View("QuizComplete");


            // Quiz time has expired
            if(quiz.TimeStarted.HasValue && quiz.TimeStarted.Value.AddMinutes(quiz.TimeLimit) < DateTime.Now) {
                quiz.TimeCompleted = quiz.TimeStarted.Value.AddMinutes(quiz.TimeLimit);
                _db.SaveChanges();
                return View("QuizComplete");
            }
              

            // If quiz not started, then record the start time
            if (!quiz.TimeStarted.HasValue)
            {
                quiz.TimeStarted = DateTime.Now;
                _db.SaveChanges();
            }

            // Find the first unanswered question and send it to the view
            QuizQuestion question = _db.QuizQuestions.Where(x => x.QuizId == id && x.Answer==null).Include(x => x.Question).ThenInclude(y => y.Topic).OrderBy(x => x.QuestionNumber).FirstOrDefault();

            // If no next question, the redirect back to list of quizes
            if (question == null)
            {
                return RedirectToAction("assessment");
            }
            else
            {
                question.TimePresented = DateTime.Now;
                _db.SaveChanges();
            }

            var TimeRemaining = (new TimeSpan(0, quiz.TimeLimit, 0)).Subtract(DateTime.Now.Subtract(quiz.TimeStarted.Value));
            ViewBag.EndDate = DateTime.Now.Add(TimeRemaining).ToString("dd-MM-yyyy h:mm:ss tt");
            ViewBag.TimeLimit = quiz.TimeLimit;
            ViewBag.QTopic = _db.Topics.Where(x => x.TopicId == quiz.TopicId).Include(x => x.Name);
            return View(question);
        }

        [HttpPost]
        public IActionResult Quiz(QuizQuestion questionAnswered)
        {
            // Get the assessment from the db based on access code cookie.
            string accessCode = GetAccessCodeCookie();

            if (string.IsNullOrEmpty(accessCode))
                return HttpUnauthorized();

            Assessment assessment = _db.Assessments.Where(x => x.AccessCode == accessCode).FirstOrDefault();

            if (assessment == null)
                return HttpNotFound();

            // Find the question that was just answered
            QuizQuestion quizQuestion = _db.QuizQuestions.Include(x => x.Question).Include(x => x.Quiz).Where(x => x.QuizQuestionId == questionAnswered.QuizQuestionId).FirstOrDefault();
            Quiz quiz = quizQuestion.Quiz;

            // Validation
            // Quiz is a part of the assessment of the corresponding access code cookie
            if (quiz.AssessmentId != assessment.AssessmentId)
                return new HttpUnauthorizedResult();

            // Quiz is complete
            if (quiz.TimeCompleted != null)
                return View("QuizComplete");


            // Quiz time has expired
            if (quiz.TimeStarted.HasValue && quiz.TimeStarted.Value.AddMinutes(quiz.TimeLimit) < DateTime.Now)
            {
                quiz.TimeCompleted = quiz.TimeStarted.Value.AddMinutes(quiz.TimeLimit);
                _db.SaveChanges();
                return View("QuizComplete");
            }

            


            // Save the answer
            if (quizQuestion.Answer == null)
            {
                quizQuestion.Answer = questionAnswered.Answer;
                quizQuestion.TimeAnswered = DateTime.Now;
                quizQuestion.Question.TimesAnswered++;

                if (string.Equals(quizQuestion.Answer, quizQuestion.Question.CorrectAnswer, StringComparison.CurrentCultureIgnoreCase))
                {
                    quiz.NumberCorrect++;
                    if (quizQuestion.Question.QuestionType == QuestionTypes.FillInBlank)
                    {
                        quizQuestion.Question.ASelected++;
                    }
                }
                  

                switch (quizQuestion.Answer)
                {
                    case "A":
                        quizQuestion.Question.ASelected++;
                        break;
                    case "B":
                        quizQuestion.Question.BSelected++;
                        break;
                    case "C":
                        quizQuestion.Question.CSelected++;
                        break;
                    case "D":
                        quizQuestion.Question.DSelected++;
                        break;
                    default :
                        break;
                }

                _db.SaveChanges();
            }

            // Calculate time remaining
            ViewBag.TimeLimit = quiz.TimeLimit;
            var TimeRemaining = (new TimeSpan(0, quiz.TimeLimit, 0)).Subtract(DateTime.Now.Subtract(quiz.TimeStarted.Value));
            ViewBag.EndDate = DateTime.Now.Add(TimeRemaining).ToString("dd-MM-yyyy h:mm:ss tt");

            // Get the next question
            QuizQuestion nextQuestion = _db.QuizQuestions.Where(x => x.QuizQuestionId == quizQuestion.NextQuestionId).Include(x => x.Question).ThenInclude(y => y.Topic).FirstOrDefault();
       
            // If no next question, the redirect back to list of quizes
            if (nextQuestion == null) {
                quiz.TimeCompleted = DateTime.Now;
                _db.SaveChanges();
                return RedirectToAction("assessment");
            }

            // Save the time we presented this question
            nextQuestion.TimePresented = DateTime.Now;
            _db.SaveChanges();

            return View(nextQuestion);

        }

        public string GetAccessCodeCookie()
        {
            
            return this.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "AccessCode").Value;

        }

        public void SetAccessCodeCookie(string accessCode)
        {
            this.HttpContext.Response.Cookies.Append("AccessCode", accessCode);

        }
    }
}
