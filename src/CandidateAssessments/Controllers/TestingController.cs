using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Http;
using CandidateAssessments.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CandidateAssessments.Controllers
{
    [AllowAnonymous]
    public class TestingController : Controller
    {
        private AssessmentContext _db;

        public TestingController(AssessmentContext context)
        {
            _db = context;
        }

        // GET: /testing/
        public IActionResult Index()
        {
            return View(new Assessment());
        }

        // GET: /testing/assessment/{code}
        public IActionResult Assessment(string accessCode)
        {
            if (accessCode == null)
            {
                return HttpNotFound();
            }

            // if access code is not null, then save it as a cookie.
            // if it is null then make sure there is already a cookie set.

            // retrieve accessment by access code alternate key (be sure to include the quiz list)
            Assessment assessment =_db.Assessments.SingleOrDefault(a => a.AccessCode == accessCode);
            // validate that the access code has not expired

            // return the assessment to the view
            return View(assessment);
        }

        // GET: /testing/quiz/id
        public IActionResult Quiz(int id)
        {
            // Get the quiz from the db by id

            // Get the assessment from the db based on access code cookie.

            // Validation
            // Quiz is a part of the assessment of the corresponding access code cookie
            // Quiz is not yet complete
            // Quiz time has not expired

            // If quiz not started, then record the start time

            // Find the first unanswered question and send it to the view
            QuizQuestion nextQuestion = null;

            return View(nextQuestion);
        }

        [HttpPost]
        public IActionResult Quiz(QuizQuestion question)
        {
            // Get the quiz from the db by question.quizid

            // Get the assessment from the db based on access code cookie.

            // Validation
            // Quiz is a part of the assessment of the corresponding access code cookie
            // Quiz is not yet complete
            // Quiz time has not expired

            // Ensure question has not already been answered - if so, then skip to next question

            // Record the answer to this question in the db

            // Update the number correct field in the quiz

            // Update the time answered field

            // Retrieve the next question
            QuizQuestion nextQuestion = null;

            // Update time presented field

            // If no next question, then record completion time, redirect to assessment actionresult


            return View(nextQuestion);
        }

        public string GetAccessCodeCookie()
        {
            
            return this.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == "AccessCode").Value;

        }

        public void SetAccessCodeCookie(string accessCode)
        {
            this.HttpContext.Response.Cookies.Append("AccessCode", accessCode);

        }
    }
}
