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

            Assessment assessment = _context.Assessments.Where(m => m.AssessmentId == id).Include(m => m.Quizes).ThenInclude(y => y.Topic).FirstOrDefault();
            if (assessment == null)
            {
                return HttpNotFound();
            }

            return View(assessment);
        }

        // GET: Assessments/Quiz/
        public IActionResult Quiz(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Quiz quiz = _context.Quizes.Where(m => m.QuizId == id).Include(m => m.Topic).FirstOrDefault();
            if (quiz == null)
            {
                return HttpNotFound();
            }

            return View(quiz);
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
                assessment.AccessCode = Guid.NewGuid().ToString();
                assessment.CreatedDate = DateTime.Now;
                assessment.ExpirationDate = DateTime.Now.AddDays(7);

                foreach (string TopicIdString in Topics)
                {
                    int TopicIdInt = int.Parse(TopicIdString);
                    _context.Quizes.Add(
                        new Quiz()
                        {
                            Assessment = assessment,
                           
                            Topic = _context.Topics.Single(m => m.TopicId == TopicIdInt),
                            NumberOfQuestions = 20,
                            NumberCorrect = 0,
                            TimeLimit = 10,
                            TimeStarted = null,
                            TimeCompleted = null,
                        });

                }
                _context.Assessments.Add(assessment);
                _context.SaveChanges();

                // TODO: This should add random questions to the quizes.
                foreach (Quiz q in _context.Quizes.Where(q => q.AssessmentId == assessment.AssessmentId).ToList())
                {
                    int i = 1;
                    foreach (TopicQuestion tq in _context.TopicQuestions.Where(x => x.TopicId == q.TopicId))
                    {


                        // Unanswered questions

                        _context.QuizQuestions.Add(
                        new QuizQuestion()
                        {
                            Quiz = q,
                            QuestionNumber = i++,
                            NextQuestionId = 0,
                            Question = tq,
                        });


                    }
                    _context.SaveChanges();
                    int nextId = 0;
                    foreach (QuizQuestion qq in q.Questions.OrderByDescending(x => x.QuestionNumber))
                    {
                        qq.NextQuestionId = nextId;
                        nextId = qq.QuizQuestionId;
                    }
                }
              
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
