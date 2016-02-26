using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using CandidateAssessments.Models;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateAssessments.Controllers
{
    [Authorize(Roles = "QuestionManager,Admin")]
    public class TopicController : Controller
    {
        private AssessmentContext _db;

        public TopicController(AssessmentContext context)
        {
            _db = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.Questions = _db.TopicQuestions.ToList();
            return View(_db.Topics.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Topic topic)
        {
            if(ModelState.IsValid)
            {
                _db.Topics.Add(topic);
                topic.Questions = new List<TopicQuestion>();
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topic);
        }

        // GET: Topics/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var qs = _db.TopicQuestions.Where(x => x.TopicId == id);
            if (qs.Count() != 0)
            {
                return HttpNotFound();
            }
          Topic topic = _db.Topics.SingleOrDefault(m => m.TopicId == id);
            if (topic == null)
            {
                return HttpNotFound();
            }

            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            Topic topic = _db.Topics.SingleOrDefault(m => m.TopicId == id);

            _db.Topics.Remove(topic);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
