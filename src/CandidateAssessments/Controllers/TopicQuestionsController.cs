using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using CandidateAssessments.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

namespace WebApplication1.Controllers
{

    [Authorize(Roles = "QuestionManager,Admin")]
    public class TopicQuestionsController : Controller
    {
        private AssessmentContext _context;

        public TopicQuestionsController(AssessmentContext context)
        {
            _context = context;
        }

        // GET: TopicQuestions
        public IActionResult Index(string searchParam, int? TopicId, int? page)
        {
            if (searchParam != null && page==0)
            {
                page = 1;
            }
            List<TopicQuestion> assessmentContext;
            List<TopicQuestion> names;
            if (TopicId != null)
            {
                assessmentContext = _context.TopicQuestions.Where(tq => tq.TopicId == TopicId).Include(t => t.Topic).ToList();

            }
            else {
                assessmentContext = _context.TopicQuestions.Include(t => t.Topic).ToList();
            }
            names = assessmentContext;
            if (searchParam != null)
            {
                assessmentContext = assessmentContext.Where(x => x.QuestionText.ToLower().Contains(searchParam.ToLower()) || x.ChoiceA.ToLower().Contains(searchParam.ToLower()) ||
                x.ChoiceB.ToLower().Contains(searchParam.ToLower()) || x.ChoiceC.ToLower().Contains(searchParam.ToLower()) || x.ChoiceD.ToLower().Contains(searchParam.ToLower()) ||
               x.Topic.Name.ToLower().Contains(searchParam.ToLower())).ToList();
            }


            ViewBag.names = names;
            ViewBag.TopicId = TopicId;

            var pageSize = 5;
            int pageNumber = (page ?? 1);
            int end = pageSize * pageNumber;
            end = (end > assessmentContext.Count()) ? assessmentContext.Count() : end;
            int start = (end % 5 > 0) ? end - (end % 5) : end - 5;
            var output = new List<TopicQuestion>();
            if (assessmentContext.Count() != 0)
            {
                for (int i = start; i < end; i++)
                {
                    output.Add(assessmentContext[i]);
                }
            }
            ViewBag.count = assessmentContext.Count();
            ViewBag.search = searchParam;
            ViewBag.page = pageNumber;
            return View(output);
        }

        // GET: TopicQuestions/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TopicQuestion topicQuestion = _context.TopicQuestions.Single(m => m.TopicQuestionId == id);
            if (topicQuestion == null)
            {
                return HttpNotFound();
            }

            return View(topicQuestion);
        }

        // GET: TopicQuestions/Create
        public IActionResult Create(int? id)
        {

            ViewBag.TopicList = _context.Topics.ToList();
            ViewBag.TopicId = id;
            return View();
        }

        // POST: TopicQuestions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TopicQuestion topicQuestion)
        {
            if (ModelState.IsValid)
            {
                var top = _context.Topics.Single(t => t.TopicId == topicQuestion.TopicId);
                topicQuestion.Topic = top;
                //not sure when topic.Questions gets initiated
                if (top.Questions == null)
                    top.Questions = new List<TopicQuestion>();
                top.Questions.Add(topicQuestion);
                _context.TopicQuestions.Add(topicQuestion);
                if (topicQuestion.QuestionType == QuestionTypes.TrueFalse)
                {
                    topicQuestion.ChoiceA = "True";
                    topicQuestion.ChoiceB = "False";
                }
                _context.SaveChanges();
                return RedirectToAction("Index", new { TopicId = top.TopicId });
            }
            ViewBag.TopicList = _context.Topics.ToList();
            return View(topicQuestion);
        }

        // GET: TopicQuestions/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TopicQuestion topicQuestion = _context.TopicQuestions.Single(m => m.TopicQuestionId == id);
            if (topicQuestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicList = _context.Topics.ToList();
            return View(topicQuestion);
        }

        // POST: TopicQuestions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TopicQuestion topicQuestion)
        {
            if (ModelState.IsValid)
            {
                // if topic was changes need to remove question from original topic list
                var top = _context.Topics.Single(t => t.TopicId == topicQuestion.TopicId);
                topicQuestion.Topic = top;

                //not sure when topic.Questions gets initiated
                if (top.Questions == null)
                    top.Questions = new List<TopicQuestion>();
                top.Questions.Add(topicQuestion);
                if (topicQuestion.QuestionType == QuestionTypes.TrueFalse)
                {
                    topicQuestion.ChoiceA = "True";
                    topicQuestion.ChoiceB = "False";
                }
                _context.Update(topicQuestion);
                _context.SaveChanges();
                return RedirectToAction("Index", new { TopicId = top.TopicId });
            }
            ViewBag.TopicList = _context.Topics.ToList();
            return View(topicQuestion);
        }

        // GET: TopicQuestions/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            TopicQuestion topicQuestion = _context.TopicQuestions.SingleOrDefault(m => m.TopicQuestionId == id);
            if (topicQuestion == null)
            {
                return HttpNotFound();
            }

            return View(topicQuestion);
        }

        // POST: TopicQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TopicQuestion topicQuestion = _context.TopicQuestions.Single(m => m.TopicQuestionId == id);
            var top = _context.Topics.Single(t => t.TopicId == topicQuestion.TopicId);

            // only get quizzes with the same topic
            var QuizQuestions = _context.Quizes.Where(x => x.TopicId == topicQuestion.TopicId).Include(x => x.Questions);
            var included = false;

            // for each quiz of this topic
            foreach (var quiz in QuizQuestions)
            {
                // for each question in that quiz
                foreach (var question in quiz.Questions)
                {
                    // this question is included in a quiz somewhere
                    if (question.TopicQuestionId == topicQuestion.TopicQuestionId)
                    {
                        included = true;
                        break;
                    }
                }
                // weve already found one
                if (included)
                    break;
            }

            // if the topic has questions
            if (top.Questions != null)
            {
                // if the question isnt included in any quiz
                if (!included)
                {
                    top.Questions.Remove(topicQuestion);
                    _context.TopicQuestions.Remove(topicQuestion);
                }
                // otherwise, just make the question inactive
                else
                {
                    topicQuestion.IsActive = false;
                }

            }

            _context.SaveChanges();
            return RedirectToAction("Index", new { TopicId = top.TopicId });
        }
    }
}
