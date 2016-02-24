using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using CandidateAssessments.Models;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class TopicQuestionsController : Controller
    {
        private AssessmentContext _context;

        public TopicQuestionsController(AssessmentContext context)
        {
            _context = context;    
        }

        // GET: TopicQuestions
        public IActionResult Index(int? TopicId)
        {
            List<TopicQuestion> assessmentContext; 
            if (TopicId != null)
            {
                assessmentContext = _context.TopicQuestions.Where(tq=>tq.TopicId==TopicId).Include(t => t.Topic).ToList();
            }
            else {
                assessmentContext = _context.TopicQuestions.Include(t => t.Topic).ToList();
            }
            ViewBag.TopicId = TopicId;
            return View(assessmentContext);
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

            TopicQuestion topicQuestion = _context.TopicQuestions.Single(m => m.TopicQuestionId == id);
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

            // if the topic has questions
            if (top.Questions != null)
            {
                // if the question has no answers, remove it
                if (topicQuestion.TimesAnswered == 0)
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
