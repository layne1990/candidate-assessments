using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessments.Models
{
    public class QuizQuestion
    {
        public int QuizQuestionId { get; set; }

        public string Answer { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public int TopicQuestionId { get; set; }
        public TopicQuestion Question { get; set; }
    }
}
