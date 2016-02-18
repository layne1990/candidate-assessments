﻿using System;
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
                return new HttpUnauthorizedResult();

            // Quiz is not yet complete
            if(quiz.TimeCompleted != null)
                return new HttpUnauthorizedResult();


            // Quiz time has not expired
//            if(quiz.TimeStarted.HasValue && quiz.TimeStarted.Value.AddMinutes(quiz.TimeLimit) < DateTime.Now )
//                return new HttpUnauthorizedResult();

            // If quiz not started, then record the start time
            if(!quiz.TimeStarted.HasValue)
            {
                quiz.TimeStarted = DateTime.Now;
                _db.SaveChanges();
            }

            // Find the first unanswered question and send it to the view
            QuizQuestion question = _db.QuizQuestions.Include(x => x.Question).Where(x => x.QuizId == id && x.Answer=="").OrderBy(x => x.QuestionNumber).FirstOrDefault();

            ViewBag.TimeRemaining = quiz.TimeLimit - quiz.TimeStarted.Value.Subtract(DateTime.Now).Minutes;
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

            // Quiz is not yet complete
            if (quiz.TimeCompleted != null)
                return new HttpUnauthorizedResult();


            // Quiz time has not expired
            //            if(quiz.TimeStarted.HasValue && quiz.TimeStarted.Value.AddMinutes(quiz.TimeLimit) < DateTime.Now )
            //                return new HttpUnauthorizedResult();



            // Save the answer
            if(quizQuestion.Answer == "")
            {
                quizQuestion.Answer = questionAnswered.Answer;
                quizQuestion.TimeAnswered = DateTime.Now;

                if (quizQuestion.Answer == quizQuestion.Question.CorrectAnswer)
                    quiz.NumberCorrect++;

                _db.SaveChanges();
            }

            // Calculate time remaining
            ViewBag.TimeRemaining = quiz.TimeLimit - quiz.TimeStarted.Value.Subtract(DateTime.Now).Minutes;

            // Get the next question
            QuizQuestion nextQuestion = _db.QuizQuestions.Include(x => x.Question).Where(x => x.QuizQuestionId == quizQuestion.NextQuestionId).FirstOrDefault();

            // If no next question, the redirect back to list of quizes
            if (nextQuestion == null)
                return RedirectToAction("assessment");

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
