using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessments.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }

        public List<TopicQuestion> Questions { get; set; }
    }
}
