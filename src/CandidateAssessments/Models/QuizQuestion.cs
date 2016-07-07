using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessments.Models
{
    public class QuizQuestion
    {
        public int QuizQuestionId { get; set; }

        public String Answer { get; set; }

        //public int EssayScore { get; set; }

        public DateTime? TimePresented { get; set; }
        public DateTime? TimeAnswered { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public int TopicQuestionId { get; set; }
        public TopicQuestion Question { get; set; }

        // An ordinal number for the question starting at 1.
        public int QuestionNumber { get; set; }

        // The id of the next quiz question
        public int NextQuestionId { get; set; }
    }
}
