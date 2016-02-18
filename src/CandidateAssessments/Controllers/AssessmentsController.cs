using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using CandidateAssessments.Models;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class AssessmentsController : Controller
    {
        private AssessmentContext _context;

        public AssessmentsController(AssessmentContext context)
        {
            _context = context;
        }

        // GET: Assessments
        public IActionResult Index()
        {
            return View(_context.Assessments.ToList());
        }

        // GET: Assessments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Assessment assessment = _context.Assessments.Single(m => m.AssessmentId == id);
            if (assessment == null)
            {
                return HttpNotFound();
            }

            return View(assessment);
        }

        // GET: Assessments/Create
        public IActionResult Create()
        {
            ViewBag.Topics = _context.Topics.ToList();
            
            return View();
        }

        // POST: Assessments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Assessment assessment, string[] Topics)
        {
            if (ModelState.IsValid)
            {
                // Generate the times and Access Code
                assessment.AccessCode     = Guid.NewGuid().ToString();
                assessment.CreatedDate    = DateTime.Now;
                assessment.ExpirationDate = DateTime.Now.AddDays(7);

                foreach(string TopicIdString in Topics)
                {
                    int TopicIdInt = int.Parse(TopicIdString);
                    Quiz Quiz = new Quiz();
                    Quiz.Topic = _context.Topics.Single(m => m.TopicId == TopicIdInt);
                    Quiz.NumberOfQuestions = 20;
                    Quiz.TimeLimit = 10;
                    Quiz.Assessment = assessment;

                    assessment.Quizes.Add(Quiz);
                }

                _context.Assessments.Add(assessment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            // pass TopicList to ViewBag for Create View
            ViewBag.TopicList = _context.Topics.ToList();
            return View(assessment);
        }

        // GET: Assessments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Assessment assessment = _context.Assessments.Single(m => m.AssessmentId == id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // POST: Assessments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                _context.Update(assessment);

                // TODO: this is throwing an error when trying to update.
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assessment);
        }

        // GET: Assessments/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Assessment assessment = _context.Assessments.Single(m => m.AssessmentId == id);
            if (assessment == null)
            {
                return HttpNotFound();
            }

            return View(assessment);
        }

        // POST: Assessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Assessment assessment = _context.Assessments.Single(m => m.AssessmentId == id);
            _context.Assessments.Remove(assessment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
