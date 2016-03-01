using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using CandidateAssessments.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Authorization;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin,Recruiter")]
    public class AssessmentsController : Controller
    {
        private AssessmentContext _context;

        public AssessmentsController(AssessmentContext context)
        {
            _context = context;
        }

        // GET: Assessments
        public IActionResult Index(String searchParam, int? page)
        {
            if (searchParam != null && page == 0)
            {
                page = 1;
            }

            var list = _context.Assessments.ToList();
            var topics = _context.Topics.ToList();
            var names = new List<Assessment>(list);
          
            if (searchParam != null)
            {
                list = list.Where(x => x.CandidateName.ToLower().Contains(searchParam.ToLower())).ToList();
            }

            ViewBag.names = names;
            ViewBag.Quizzes = _context.Quizes.Include(x => x.Topic).ToList();



            var pageSize = 5;
            int pageNumber = (page ?? 1);
            int end = pageSize * pageNumber;
            end = (end > list.Count()) ? list.Count() : end;
            int start = (end % 5 > 0) ? end - (end % 5) : end - 5;
            var output = new List<Assessment>();
            if (list.Count() != 0)
            {
                for (int i = start; i < end; i++)
                {
                    output.Add(list[i]);
                }
            }
            ViewBag.count = list.Count();
            ViewBag.search = searchParam;
            ViewBag.page = pageNumber;
            return View(output);
        }

        // GET: Assessments/Code/5
        public IActionResult Code(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Assessment assessment = _context.Assessments.Where(m => m.AssessmentId == id).FirstOrDefault();
            if (assessment == null)
            {
                return HttpNotFound();
            }
            ViewBag.url = Request.Host;
            return View(assessment);
        }

        // GET: Assessments/Quiz/
        public IActionResult Quiz(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Quiz quiz = _context.Quizes.Where(m => m.QuizId == id).Include(m => m.Topic).Include(m => m.Questions).ThenInclude(y => y.Question).FirstOrDefault();
            if (quiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.assessment = _context.Assessments.Where(m => m.AssessmentId == quiz.AssessmentId).FirstOrDefault();
            return View(quiz);
        }

        // GET: Assessments/Create
        public IActionResult Create()
        {
            ViewBag.Topics = _context.Topics.Where(x => x.Questions.Where(y => y.IsActive == true).Count() != 0);

            return View();
        }

        // POST: Assessments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Assessment assessment, string[] Topics, int TimeLimit, int NumQuestions)
        {
            if (ModelState.IsValid)
            {
                if (TimeLimit == 0)
                {
                    TimeLimit = 10;
                }
                if (NumQuestions == 0)
                {
                    NumQuestions = 20;
                }
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
                            NumberOfQuestions = NumQuestions,
                            NumberCorrect = 0,
                            TimeLimit = TimeLimit,
                            TimeStarted = null,
                            TimeCompleted = null,
                        });

                }
                _context.Assessments.Add(assessment);
                _context.SaveChanges();

                foreach (Quiz q in _context.Quizes.Where(q => q.AssessmentId == assessment.AssessmentId).ToList())
                {
                    int i = 1;
                    var List = _context.TopicQuestions.Where(x => x.TopicId == q.TopicId && x.IsActive == true).ToList();
                    Random rand = new Random();
                    var QsUsed = new List<int>();
                    while (QsUsed.Count < NumQuestions && QsUsed.Count != List.Count)
                    {
                        int r = rand.Next(0, List.Count());
                        if (!QsUsed.Contains(r))
                        {
                            _context.QuizQuestions.Add(
                        new QuizQuestion()
                        {
                            Quiz = q,
                            QuestionNumber = i++,
                            NextQuestionId = 0,
                            Question = List[r],
                        });
                            QsUsed.Add(r);
                        }
                    }

                    _context.SaveChanges();
                    int nextId = 0;
                    foreach (QuizQuestion qq in q.Questions.OrderByDescending(x => x.QuestionNumber))
                    {
                        qq.NextQuestionId = nextId;
                        nextId = qq.QuizQuestionId;
                    }

                    if (q.Questions.Count < q.NumberOfQuestions)
                        q.NumberOfQuestions = q.Questions.Count;

                }

                _context.SaveChanges();
                return RedirectToAction("Code", new { id = assessment.AssessmentId });
            }

            // pass TopicList to ViewBag for Create View
            ViewBag.Topics = _context.Topics.Where(x => x.Questions.Where(y => y.IsActive == true).Count() != 0);
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

            Assessment assessment = _context.Assessments.SingleOrDefault(m => m.AssessmentId == id);
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

            foreach (Quiz quiz in _context.Quizes.Where(q => q.AssessmentId == assessment.AssessmentId).ToList())
            {
                _context.Quizes.Remove(quiz);
            }

            _context.Assessments.Remove(assessment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
