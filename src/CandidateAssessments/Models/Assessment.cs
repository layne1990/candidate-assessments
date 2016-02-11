using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessments.Models
{
    public class Assessment
    {
        public int AssessmentId { get; set; }
        public string CandidateName { get; set; }
        public string AccessCode { get; set; }

        public List<Quiz> Quizes { get; set; }
    }
}
