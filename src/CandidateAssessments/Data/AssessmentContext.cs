using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CandidateAssessments.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CandidateAssessments.Data
{
    public class AssessmentContext : DbContext
    {
        public AssessmentContext(DbContextOptions<AssessmentContext> options)
            : base(options)
        {
        }

        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<TopicQuestion> TopicQuestions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // RC1 to RTM changed how the SQL table names are generated
            // This would cause a problem when we go to migrate the DB.
            // This loop will revert to RC1 naming.
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            // Can't delete a topic question if there are any quizes that use that question
            modelBuilder.Entity<QuizQuestion>()
                .HasOne(tq => tq.Question)
                .WithMany()
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict);
            

            // So we can quickly find an assessment by the acccess code
            modelBuilder.Entity<Assessment>().HasAlternateKey(a => a.AccessCode);
        }

        
    }
}
