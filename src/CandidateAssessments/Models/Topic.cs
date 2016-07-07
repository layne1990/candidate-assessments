using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CandidateAssessments.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        public Boolean Active { get; set; }
        public List<TopicQuestion> Questions { get; set; }

        //public int DefaultNumberOfQuestions { get; set; }
        //public int DefaultTimeLimit { get; set; }

    }
}
