using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using CandidateAssessments.Models;

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
            return View();
        }

        // POST: Assessments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                _context.Assessments.Add(assessment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
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
