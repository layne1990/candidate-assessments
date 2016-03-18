using System;
using System.ComponentModel.DataAnnotations;


namespace CandidateAssessments.Models
{
   
   
    public enum QuestionTypes {[Display(Name = "Multiple choice")]MultipleChoice, [Display(Name = "True or False")]TrueFalse, [Display(Name = "Fill in the Blank")]FillInBlank }
   public enum DifficultyLevels {Easy,Medium,Hard }
    public class TopicQuestion
    {
        public int TopicQuestionId { get; set; }
        [Required]
        [Display(Name = "Question Text")]
        [DataType(DataType.MultilineText)]
        public string QuestionText { get; set; }
        [Required]
        [Display(Name ="Correct Answer")]
        [MaxLength(50)]
        public string CorrectAnswer { get; set; }

        // Determines if this question will be used in new quizes
        [Display(Name = "is Active?")]
        public bool IsActive { get; set; }
        [Display(Name = "Question Type")]
        public QuestionTypes QuestionType { get; set; }
        [Display(Name = "Difficulty Level")]
        public DifficultyLevels DifficultyLevel { get; set; }
        // Used for multiple choice questions
        [Required]
        [Display(Name = "Choice A")]
        public string ChoiceA { get; set; }
        [Required]
        [Display(Name = "Choice B")]
        public string ChoiceB { get; set; }
        [Required]
        [Display(Name = "Choice C")]
        public string ChoiceC { get; set; }
        [Required]
        [Display(Name = "Choice D")]
        public string ChoiceD { get; set; }
        public TimeSpan? TotalTime { get; set; }
        public int TimesAnswered { get; set; }
        public int ASelected { get; set; }
        public int BSelected { get; set; }
        public int CSelected { get; set; }
        public int DSelected { get; set; }

        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}
