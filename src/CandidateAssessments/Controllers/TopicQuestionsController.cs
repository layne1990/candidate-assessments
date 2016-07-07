using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CandidateAssessments.Models;
using CandidateAssessments.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System;

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
        public IActionResult Index(string searchParam, int topicId=0, int page=1)
        {
            var pageSize = 5;

            // If a topic wasn't provided, then go back to the topic list
            if (topicId == 0)
                return RedirectToAction("Index", "Topics");

            var questionQuery = _context.TopicQuestions.Where(x => x.TopicId == topicId);
            if (searchParam != null)
            {
                questionQuery = questionQuery.Where(x => x.QuestionText.ToLower().Contains(searchParam.ToLower()) || 
                x.TopicQuestionId.ToString().Contains(searchParam.ToLower()) ||
                x.DifficultyLevel.ToString().ToLower().Contains(searchParam.ToLower()) 
               );
            }


            var questions = questionQuery.Include(x => x.Topic)
               .OrderBy(x => x.TopicQuestionId)
               .Skip(pageSize * (page - 1))
               .Take(pageSize)
               .ToList();


            ViewBag.pages = (int)Math.Ceiling(questionQuery.Count() / (double)pageSize);
            ViewBag.search = searchParam;
            ViewBag.page = page;
            ViewBag.TopicId = topicId;
            return View(questions);
        }

        // GET: TopicQuestions/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundResult();
            }

            TopicQuestion topicQuestion = _context.TopicQuestions.Where(m => m.TopicQuestionId == id).Include(y => y.Topic).SingleOrDefault();
            if (topicQuestion == null)
            {
                return new NotFoundResult();
            }

            return View(topicQuestion);
        }

        // GET: TopicQuestions/Create
        public IActionResult Create(int? id)
        {

            ViewBag.TopicList = _context.Topics.ToList();
            ViewBag.TopicId = id;
            TopicQuestion topicQuestion = new TopicQuestion();
            topicQuestion.IsActive = true;
            return View(topicQuestion);
        }

        // POST: TopicQuestions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TopicQuestion topicQuestion)
        {
            if (ModelState.IsValid)
            {
                // Initilize some statistics for this topic questions
                topicQuestion.TotalTime = new System.TimeSpan(0, 0, 0);
                topicQuestion.TimesAnswered = 0;
                topicQuestion.ASelected = 0;
                topicQuestion.BSelected = 0;
                topicQuestion.CSelected = 0;
                topicQuestion.DSelected = 0;

                _context.TopicQuestions.Add(topicQuestion);

                _context.SaveChanges();
                return RedirectToAction("Details", new { id = topicQuestion.TopicQuestionId });
            }
            ViewBag.TopicList = _context.Topics.ToList();
            return View(topicQuestion);
        }

        // GET: TopicQuestions/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundResult();
            }

            TopicQuestion topicQuestion = _context.TopicQuestions.Single(m => m.TopicQuestionId == id);
            if (topicQuestion == null)
            {
                return new NotFoundResult();
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
                _context.Update(topicQuestion);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = topicQuestion.TopicQuestionId });
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
                return new NotFoundResult();
            }

            TopicQuestion topicQuestion = _context.TopicQuestions.SingleOrDefault(m => m.TopicQuestionId == id);
            if (topicQuestion == null)
            {
                return new NotFoundResult();
            }

            return View(topicQuestion);
        }

        // POST: TopicQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            TopicQuestion topicQuestion = _context.TopicQuestions.SingleOrDefault(m => m.TopicQuestionId == id);
            
            // Check to see if this question has been used on a quiz
            if(_context.QuizQuestions.Where(q=> q.TopicQuestionId == id).Count() > 0)
            {
                // Just mark as inactive
                topicQuestion.IsActive = false;
            }
            else
            {
                // Ok to delete
                _context.TopicQuestions.Remove(topicQuestion);

            }

            _context.SaveChanges();
            
            
            return RedirectToAction("Index", new { TopicId = topicQuestion.TopicId });
        }
    }
}
