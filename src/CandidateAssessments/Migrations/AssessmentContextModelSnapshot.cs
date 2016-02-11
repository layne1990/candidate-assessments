using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using CandidateAssessments.Models;

namespace CandidateAssessments.Migrations
{
    [DbContext(typeof(AssessmentContext))]
    partial class AssessmentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CandidateAssessments.Models.Assessment", b =>
                {
                    b.Property<int>("AssessmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessCode");

                    b.Property<string>("CandidateName");

                    b.HasKey("AssessmentId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.Quiz", b =>
                {
                    b.Property<int>("QuizId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssessmentId");

                    b.Property<int>("NumberCorrect");

                    b.Property<int>("NumberOfQuestions");

                    b.Property<int>("TimeLimit");

                    b.Property<int>("TimeTaken");

                    b.Property<int>("TopicId");

                    b.HasKey("QuizId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.QuizQuestion", b =>
                {
                    b.Property<int>("QuizQuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<int>("QuizId");

                    b.Property<int>("TopicQuestionId");

                    b.HasKey("QuizQuestionId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("TopicId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.TopicQuestion", b =>
                {
                    b.Property<int>("TopicQuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ChoiceA");

                    b.Property<string>("ChoiceB");

                    b.Property<string>("ChoiceC");

                    b.Property<string>("ChoiceD");

                    b.Property<string>("CorrectAnswer");

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuestionText");

                    b.Property<int>("TopicId");

                    b.HasKey("TopicQuestionId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.Quiz", b =>
                {
                    b.HasOne("CandidateAssessments.Models.Assessment")
                        .WithMany()
                        .HasForeignKey("AssessmentId");

                    b.HasOne("CandidateAssessments.Models.Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.QuizQuestion", b =>
                {
                    b.HasOne("CandidateAssessments.Models.Quiz")
                        .WithMany()
                        .HasForeignKey("QuizId");

                    b.HasOne("CandidateAssessments.Models.TopicQuestion")
                        .WithMany()
                        .HasForeignKey("TopicQuestionId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.TopicQuestion", b =>
                {
                    b.HasOne("CandidateAssessments.Models.Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");
                });
        }
    }
}
