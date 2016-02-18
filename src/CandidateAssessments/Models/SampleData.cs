using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Entity;

namespace CandidateAssessments.Models
{
    public static class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<AssessmentContext>();

            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (!context.Topics.Any())
            {

                // Topics
                context.Topics.AddRange(
                    new Topic { Name = "C#" },
                    new Topic { Name = "SQL" },
                    new Topic { Name = "JavaScript" }
                );

                context.SaveChanges();

                // Topic Questions
                foreach (Topic t in context.Topics)
                {
                    for(int i = 1; i<4; i++)
                    {
                        context.TopicQuestions.Add(
                        new TopicQuestion()
                        {
                            QuestionText = "This is question " + i.ToString(),
                            ChoiceA = "This is choice A",
                            ChoiceB = "This is choice B",
                            ChoiceC = "This is choice C",
                            ChoiceD = "This is choice D",
                            CorrectAnswer = "C",
                            QuestionType = QuestionTypes.MultipleChoice,
                            IsActive = true,
                            Topic = t,
                        });
                    }
                    
                }
                context.SaveChanges();
                
                // Assessments
                context.Assessments.AddRange(
                    new Assessment { CandidateName = "John Doe", AccessCode = Guid.NewGuid().ToString(), CreatedDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(5) },
                    new Assessment { CandidateName = "Jim Beam", AccessCode = Guid.NewGuid().ToString(), CreatedDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(5) },
                    // Expired assessment
                    new Assessment { CandidateName = "Jane Smith", AccessCode = Guid.NewGuid().ToString(), CreatedDate = DateTime.Now, ExpirationDate = DateTime.Now }
                );
                context.SaveChanges();

                // Quizes                
                foreach (Assessment a in context.Assessments.ToList())
                {
                    foreach (Topic t in context.Topics.ToList())
                    {
                        context.Quizes.Add(
                        new Quiz()
                        {
                            Assessment = a,
                            Topic = t,
                            NumberOfQuestions = 20,
                            NumberCorrect = t.TopicId % 2 == 0 ? 15 : 0,
                            TimeLimit = 10,
                            TimeStarted = t.TopicId % 2 == 0 ? (DateTime?)DateTime.Now.AddDays(-1) : null,
                            TimeCompleted = t.TopicId % 2 == 0 ? (DateTime?)DateTime.Now.AddDays(-1).AddMinutes(8) : null,
                        });
                    }

                }
                context.SaveChanges();

                // Quiz Questions
                foreach (Quiz q in context.Quizes.ToList())
                {
                    int i = 1;
                    foreach (TopicQuestion tq in context.TopicQuestions)
                    {

                        if(tq.TopicId % 2 == 0)
                        {

                            // Answered questions
                            context.QuizQuestions.Add(
                            new QuizQuestion()
                            {
                                Answer = "C",
                                TimeAnswered = DateTime.Now,
                                TimePresented = DateTime.Now.AddMinutes(-1),
                                Quiz = q,
                                QuestionNumber = i++,
                                NextQuestionId = 0,
                                Question = tq,
                            });

                        }
                        else
                        {
                            // Unanswered questions

                            context.QuizQuestions.Add(
                            new QuizQuestion()
                            {
                                Quiz = q,
                                QuestionNumber = i++,
                                NextQuestionId = 0,
                                Question = tq,
                            });
                        }
                        
                    }
                    context.SaveChanges();

                    // Fill in the next question id
                    int nextId = 0;
                    foreach(QuizQuestion qq in q.Questions.OrderByDescending(x => x.QuestionNumber))
                    {
                        qq.NextQuestionId = nextId;
                        nextId = qq.QuizQuestionId;
                    }
                    context.SaveChanges();

                }
                


            }
        }
    }

}

