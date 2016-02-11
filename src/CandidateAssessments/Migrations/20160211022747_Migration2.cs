using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace CandidateAssessments.Migrations
{
    public partial class Migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Quiz_Assessment_AssessmentId", table: "Quiz");
            migrationBuilder.DropForeignKey(name: "FK_Quiz_Topic_TopicId", table: "Quiz");
            migrationBuilder.DropForeignKey(name: "FK_QuizQuestion_Quiz_QuizId", table: "QuizQuestion");
            migrationBuilder.DropForeignKey(name: "FK_TopicQuestion_Topic_TopicId", table: "TopicQuestion");
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TopicQuestion",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Assessment_AssessmentId",
                table: "Quiz",
                column: "AssessmentId",
                principalTable: "Assessment",
                principalColumn: "AssessmentId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Topic_TopicId",
                table: "Quiz",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestion_Quiz_QuizId",
                table: "QuizQuestion",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_TopicQuestion_Topic_TopicId",
                table: "TopicQuestion",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Quiz_Assessment_AssessmentId", table: "Quiz");
            migrationBuilder.DropForeignKey(name: "FK_Quiz_Topic_TopicId", table: "Quiz");
            migrationBuilder.DropForeignKey(name: "FK_QuizQuestion_Quiz_QuizId", table: "QuizQuestion");
            migrationBuilder.DropForeignKey(name: "FK_TopicQuestion_Topic_TopicId", table: "TopicQuestion");
            migrationBuilder.DropColumn(name: "IsActive", table: "TopicQuestion");
            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Assessment_AssessmentId",
                table: "Quiz",
                column: "AssessmentId",
                principalTable: "Assessment",
                principalColumn: "AssessmentId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Quiz_Topic_TopicId",
                table: "Quiz",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_QuizQuestion_Quiz_QuizId",
                table: "QuizQuestion",
                column: "QuizId",
                principalTable: "Quiz",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_TopicQuestion_Topic_TopicId",
                table: "TopicQuestion",
                column: "TopicId",
                principalTable: "Topic",
                principalColumn: "TopicId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
