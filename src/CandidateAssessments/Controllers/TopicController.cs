using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using CandidateAssessments.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateAssessments.Controllers
{
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
    }
}
