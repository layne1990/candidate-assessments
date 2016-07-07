using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CandidateAssessments.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessCode = table.Column<string>(type: "VARCHAR(36)", maxLength: 36, nullable: false),
                    CandidateName = table.Column<string>(maxLength: 128, nullable: false),
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
                    Name = table.Column<string>(maxLength: 128, nullable: false)
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
                    CorrectAnswer = table.Column<string>(maxLength: 50, nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_AssessmentId",
                table: "Quiz",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_TopicId",
                table: "Quiz",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_QuizId",
                table: "QuizQuestion",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestion_TopicQuestionId",
                table: "QuizQuestion",
                column: "TopicQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicQuestion_TopicId",
                table: "TopicQuestion",
                column: "TopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizQuestion");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "TopicQuestion");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "Topic");
        }
    }
}
