using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CandidateAssessments.Models
{
    public enum QuestionTypes {MultipleChoice, TrueFalse, FillInBlank }
   
    public class TopicQuestion
    {
        [Display(Name = "Question ID #")]
        public int TopicQuestionId { get; set; }
        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }
        [Display(Name ="Correct Answer")]
        [MaxLength(50)]
        public string CorrectAnswer { get; set; }

        // Determines if this question will be used in new quizes
        [Display(Name = "is Active?")]
        public bool IsActive { get; set; }
        [Display(Name = "Question Type")]
        public QuestionTypes QuestionType { get; set; }

        // Used for multiple choice questions
        [Display(Name = "Choice A")]
        public string ChoiceA { get; set; }
        [Display(Name = "Choice B")]
        public string ChoiceB { get; set; }
        [Display(Name = "Choice C")]
        public string ChoiceC { get; set; }
        [Display(Name = "Choice D")]
        public string ChoiceD { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
