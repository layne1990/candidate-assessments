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
        private AssessmentContext _context;

        public TopicController(AssessmentContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_context.Topics.ToList());
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
                _context.Topics.Add(topic);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topic);
        }
    }
}
