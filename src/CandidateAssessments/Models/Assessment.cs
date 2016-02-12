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

        [MaxLength(128)]
        public string CandidateName { get; set; }

        [Required]
        [MaxLength(36)]
        public string AccessCode { get; set; }

        // Probably don't need this since we can just grab the time when the user hits the create button
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        // Could probably also preset this to be a week from the day the assessment is created
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationDate { get; set; }

        public List<Quiz> Quizes { get; set; }
    }
}
