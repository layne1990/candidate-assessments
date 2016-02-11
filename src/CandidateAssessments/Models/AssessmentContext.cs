using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessments.Models
{
    public class AssessmentContext : DbContext
    {
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<TopicQuestion> TopicQuestions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Can't delete a topic question if there are any quizes that use that question
            modelBuilder.Entity<QuizQuestion>()
                .HasOne(tq => tq.Question)
                .WithMany()
                .OnDelete(Microsoft.Data.Entity.Metadata.DeleteBehavior.Restrict);
        }
    }
}
