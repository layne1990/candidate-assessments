using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessments.Models
{
    public class Quiz
    {

        public int QuizId { get; set; }

        public Topic Topic{get;set;}

        public Assessment Assessment { get; set; }

        // this can be calculated based on the length of this.Questions, do we need to keep it around?
        public int NumberOfQuestions { get; set; }
        public int NumberCorrect { get; set; }
        public int TimeLimit { get; set; }
        
        public DateTime? TimeStarted { get; set; }
        public DateTime? TimeCompleted { get; set; }

        public List<QuizQuestion> Questions { get; set; }
    }
}
