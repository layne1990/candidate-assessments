using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CandidateAssessments.Models
{
    public class Assessment
    {
        public int AssessmentId { get; set; }

        [Required]
        [Display(Name = "Candidate Name")]
        [MaxLength(128)]
        public string CandidateName { get; set; }

        [Display(Name = "Access Code")]
        [MaxLength(36)]
        public string AccessCode { get; set; }

        [Display(Name = "Date Created")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Expiration Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }

        public List<Quiz> Quizes { get; set; }
    }
}
