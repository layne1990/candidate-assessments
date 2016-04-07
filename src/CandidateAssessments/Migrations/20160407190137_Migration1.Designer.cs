using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using CandidateAssessments.Models;

namespace CandidateAssessments.Migrations
{
    [DbContext(typeof(AssessmentContext))]
    [Migration("20160407190137_Migration1")]
    partial class Migration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("ExpirationDate");

                    b.Property<string>("User");

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

                    b.Property<DateTime?>("TimeCompleted");

                    b.Property<int>("TimeLimit");

                    b.Property<DateTime?>("TimeStarted");

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

                    b.Property<DateTime?>("TimeAnswered");

                    b.Property<DateTime?>("TimePresented");

                    b.Property<int>("TopicQuestionId");

                    b.HasKey("QuizQuestionId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.Topic", b =>
                {
                    b.Property<int>("TopicId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.HasKey("TopicId");
                });

            modelBuilder.Entity("CandidateAssessments.Models.TopicQuestion", b =>
                {
                    b.Property<int>("TopicQuestionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ASelected");

                    b.Property<int>("BSelected");

                    b.Property<int>("CSelected");

                    b.Property<string>("ChoiceA");

                    b.Property<string>("ChoiceB");

                    b.Property<string>("ChoiceC");

                    b.Property<string>("ChoiceD");

                    b.Property<string>("CorrectAnswer")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int>("DSelected");

                    b.Property<int>("DifficultyLevel");

                    b.Property<bool>("IsActive");

                    b.Property<string>("QuestionText")
                        .IsRequired();

                    b.Property<int>("QuestionType");

                    b.Property<int>("TimesAnswered");

                    b.Property<int>("TopicId");

                    b.Property<TimeSpan?>("TotalTime");

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
