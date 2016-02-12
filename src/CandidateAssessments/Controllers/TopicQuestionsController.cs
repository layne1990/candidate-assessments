using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using CandidateAssessments.Models;

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
        public IActionResult Index()
        {
            var assessmentContext = _context.TopicQuestions.Include(t => t.Topic);
            return View(assessmentContext.ToList());
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
        public IActionResult Create()
        {
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Topic");
            return View();
        }

        // POST: TopicQuestions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TopicQuestion topicQuestion)
        {
            if (ModelState.IsValid)
            {
                _context.TopicQuestions.Add(topicQuestion);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Topic", topicQuestion.TopicId);
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
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Topic", topicQuestion.TopicId);
            return View(topicQuestion);
        }

        // POST: TopicQuestions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TopicQuestion topicQuestion)
        {
            if (ModelState.IsValid)
            {
                _context.Update(topicQuestion);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Topic", topicQuestion.TopicId);
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
            _context.TopicQuestions.Remove(topicQuestion);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
