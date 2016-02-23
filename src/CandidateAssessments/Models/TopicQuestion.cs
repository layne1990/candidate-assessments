﻿using System.ComponentModel.DataAnnotations;


namespace CandidateAssessments.Models
{
   
   
    public enum QuestionTypes {[Display(Name = "Multiple choice")]MultipleChoice, [Display(Name = "True or Flase")]TrueFalse, [Display(Name = "Fill In the Blank")]FillInBlank }
   
    public class TopicQuestion
    {
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

        public double PercentA { get; set; }
        public double PercentB { get; set; }
        public double PercentC { get; set; }
        public double PercentD { get; set; }
    }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
