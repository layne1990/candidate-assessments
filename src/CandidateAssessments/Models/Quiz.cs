using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessments.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public int TopicId { get; set; }

        public Topic Topic{get;set;}

        public int AssessmentId { get; set; }
        public Assessment Assessment { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberCorrect { get; set; }
        public int TimeLimit { get; set; }
        
        public DateTime TimeStarted { get; set; }
        public DateTime TimeCompleted { get; set; }


        public List<QuizQuestion> Questions { get; set; }
    }
}
