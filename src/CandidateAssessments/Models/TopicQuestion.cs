using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CandidateAssessments.Models
{
    public enum QuestionTypes { MultipleChoice, TrueFalse, FillInBlank}
    public class TopicQuestion
    {
        public int TopicQuestionId { get; set; }
        
        public string QuestionText { get; set; }
       
        [MaxLength(50)]
        public Answer CorrectAnswer { get; set; }

        // Determines if this question will be used in new quizes
        public bool IsActive { get; set; }

        public QuestionTypes QuestionType { get; set; }
        
        // Used for multiple choice questions
        public string ChoiceA { get; set; }
        public string ChoiceB { get; set; }
        public string ChoiceC { get; set; }
        public string ChoiceD { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
