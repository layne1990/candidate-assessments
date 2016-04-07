using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace CandidateAssessments.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessCode = table.Column<string>(nullable: true),
                    CandidateName = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.AssessmentId);
                    table.UniqueConstraint("AK_Assessment_AccessCode", x => x.AccessCode);
                });
            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.TopicId);
                });
            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    QuizId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssessmentId = table.Column<int>(nullable: false),
                    NumberCorrect = table.Column<int>(nullable: false),
                    NumberOfQuestions = table.Column<int>(nullable: false),
                    TimeCompleted = table.Column<DateTime>(nullable: true),
                    TimeLimit = table.Column<int>(nullable: false),
                    TimeStarted = table.Column<DateTime>(nullable: true),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.QuizId);
                    table.ForeignKey(
                        name: "FK_Quiz_Assessment_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessment",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quiz_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "TopicQuestion",
                columns: table => new
                {
                    TopicQuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ASelected = table.Column<int>(nullable: false),
                    BSelected = table.Column<int>(nullable: false),
                    CSelected = table.Column<int>(nullable: false),
                    ChoiceA = table.Column<string>(nullable: true),
                    ChoiceB = table.Column<string>(nullable: true),
                    ChoiceC = table.Column<string>(nullable: true),
                    ChoiceD = table.Column<string>(nullable: true),
                    CorrectAnswer = table.Column<string>(nullable: true),
                    DSelected = table.Column<int>(nullable: false),
                    DifficultyLevel = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    QuestionText = table.Column<string>(nullable: false),
                    QuestionType = table.Column<int>(nullable: false),
                    TimesAnswered = table.Column<int>(nullable: false),
                    TopicId = table.Column<int>(nullable: false),
                    TotalTime = table.Column<TimeSpan>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicQuestion", x => x.TopicQuestionId);
                    table.ForeignKey(
                        name: "FK_TopicQuestion_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "QuizQuestion",
                columns: table => new
                {
                    QuizQuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Answer = table.Column<string>(nullable: true),
                    NextQuestionId = table.Column<int>(nullable: false),
                    QuestionNumber = table.Column<int>(nullable: false),
                    QuizId = table.Column<int>(nullable: false),
                    TimeAnswered = table.Column<DateTime>(nullable: true),
                    TimePresented = table.Column<DateTime>(nullable: true),
                    TopicQuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestion", x => x.QuizQuestionId);
                    table.ForeignKey(
                        name: "FK_QuizQuestion_Quiz_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizQuestion_TopicQuestion_TopicQuestionId",
                        column: x => x.TopicQuestionId,
                        principalTable: "TopicQuestion",
                        principalColumn: "TopicQuestionId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("QuizQuestion");
            migrationBuilder.DropTable("Quiz");
            migrationBuilder.DropTable("TopicQuestion");
            migrationBuilder.DropTable("Assessment");
            migrationBuilder.DropTable("Topic");
        }
    }
}
