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

                    b.Property<string>("AccessCode")
                        .HasAnnotation("MaxLength", 36);

                    b.Property<string>("CandidateName")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("ExpirationDate");

                    b.HasKey("AssessmentId");

                    b.HasAlternateKey("AccessCode");
                });

            modelBuilder.Entity("CandidateAssessments.Models.Quiz", b =>
                {
                    b.Property<int>("QuizId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssessmentId");

                    b.Property<int>("NumberCorrect");

                    b.Property<int>("NumberOfQuestions");

                    b.Property<DateTime>("TimeCompleted");

                    b.Property<int>("TimeLimit");

                    b.Property<DateTime>("TimeStarted");

                    b.Property<int>("TopicId");

                    b.HasKey("QuizId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.QuizQuestion", b =>
                {
                    b.Property<int>("QuizQuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<int>("NextQuestionId");

                    b.Property<int>("QuestionNumber");

                    b.Property<int>("QuizId");

                    b.Property<DateTime>("TimeAnswered");

                    b.Property<DateTime>("TimePresented");

                    b.Property<int>("TopicQuestionId");

                    b.HasKey("QuizQuestionId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 128);

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

                    b.Property<string>("CorrectAnswer")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuestionText");

                    b.Property<int>("QuestionType");

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
