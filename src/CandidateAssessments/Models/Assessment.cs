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

        [MaxLength(36)]
        public string AccessCode { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public List<Quiz> Quizes { get; set; }
    }
}
