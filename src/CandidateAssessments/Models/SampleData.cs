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

            string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";

            var random = new Random();

            if (!context.Topics.Any())
            {

                // Topics
                context.Topics.AddRange(
                    new Topic { Name = "C#", Active = true },
                    new Topic { Name = "SQL", Active = true },
                    new Topic { Name = "JavaScript", Active = true },
                      new Topic { Name = "HTML", Active = true }
                );

                context.SaveChanges();

                // Topic Questions
                foreach (Topic t in context.Topics)
                {

                    for (int i = 1; i < 4; i++)
                    {

                        context.TopicQuestions.Add(
                        new TopicQuestion()
                        {
                            QuestionText = "This is question " + i.ToString() + ". " + lorem.Substring(0, random.Next(0, lorem.Length)),
                            ChoiceA = "This is choice A" + lorem.Substring(random.Next(0, 100), random.Next(0, 50)),
                            ChoiceB = "This is choice B" + lorem.Substring(random.Next(0, 100), random.Next(0, 50)),
                            ChoiceC = "This is choice C" + lorem.Substring(random.Next(0, 100), random.Next(0, 50)),
                            ChoiceD = "This is choice D" + lorem.Substring(random.Next(0, 100), random.Next(0, 50)),
                            TotalTime = new TimeSpan(0, 0, 0),
                            CorrectAnswer = "C",
                            QuestionType = QuestionTypes.MultipleChoice,
                            IsActive = true,
                            DifficultyLevel = DifficultyLevels.Medium,
                            Topic = t
                        });
                    }

                    for (int i = 1; i < 4; i++)
                    {

                        context.TopicQuestions.Add(
                        new TopicQuestion()
                        {
                            QuestionText = "This is question " + i.ToString() + ". " + lorem.Substring(0, random.Next(0, lorem.Length)),
                            TotalTime = new TimeSpan(0, 0, 0),
                            CorrectAnswer = "T",
                            QuestionType = QuestionTypes.TrueFalse,
                            IsActive = true,
                            DifficultyLevel = DifficultyLevels.Hard,
                            Topic = t,

                        });
                    }

                    for (int i = 1; i < 4; i++)
                    {

                        context.TopicQuestions.Add(
                        new TopicQuestion()
                        {
                            QuestionText = "This is question " + i.ToString() + ". " + lorem.Substring(0, random.Next(0, lorem.Length)),
                            TotalTime = new TimeSpan(0, 0, 0),
                            CorrectAnswer = "Answer",
                            QuestionType = QuestionTypes.FillInBlank,
                            IsActive = true,
                            DifficultyLevel = DifficultyLevels.Medium,
                            Topic = t
                        });
                    }
                    context.TopicQuestions.Add(
                      new TopicQuestion()
                      {
                          QuestionText = "This is question " + 10.ToString() + ". " + lorem.Substring(0, random.Next(0, lorem.Length)),
                          TotalTime = new TimeSpan(0, 0, 0),
                          CorrectAnswer = "Answer",
                          QuestionType = QuestionTypes.FillInBlank,
                          IsActive = true,
                          DifficultyLevel = DifficultyLevels.Easy,
                          Topic = t
                      });
                    context.TopicQuestions.Add(
                      new TopicQuestion()
                      {
                          QuestionText = "This is an essay question ",
                          TotalTime = new TimeSpan(0, 0, 0),
                          QuestionType = QuestionTypes.Essay,
                          IsActive = true,
                          DifficultyLevel = DifficultyLevels.Easy,
                          Topic = t
                      });

                }
                context.SaveChanges();

                // Assessments

                //Uncomment to add 50 assessments
                /*List<Assessment> toAdd = new List<Assessment>();
                for (int i = 0; i < 50; i++)
                {
                    toAdd.Add(new Assessment { CandidateName = "Candidate"+i, AccessCode = Guid.NewGuid().ToString(), CreatedDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(5) });
                }
                context.Assessments.AddRange(toAdd);*/

                context.Assessments.AddRange(

                    new Assessment { CandidateName = "John Doe", AccessCode = Guid.NewGuid().ToString(), CreatedDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(7), User = "user1@laynerdacorp232.onmicrosoft.com" },
                    new Assessment { CandidateName = "Jim Beam", AccessCode = Guid.NewGuid().ToString(), CreatedDate = DateTime.Now, ExpirationDate = DateTime.Now.AddDays(7), User = "user1@laynerdacorp232.onmicrosoft.com" },
                     // Expired assessment
                     new Assessment { CandidateName = "Jane Smith", AccessCode = Guid.NewGuid().ToString(), CreatedDate = DateTime.Now, ExpirationDate = DateTime.Now, User = "user1@laynerdacorp232.onmicrosoft.com" }
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
                            NumberOfQuestions = 10,
                            NumberCorrect = 0,
                            TimeLimit = 10,
                            TimeStarted = null,
                            TimeCompleted = null,
                        });
                    }

                }
                context.SaveChanges();

                // Quiz Questions
                foreach (Quiz q in context.Quizes.ToList())
                {
                    int i = 1;
                    foreach (TopicQuestion tq in context.TopicQuestions.Where(x => x.TopicId == q.TopicId))
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
                    context.SaveChanges();

                    // Fill in the next question id
                    int nextId = 0;
                    foreach (QuizQuestion qq in q.Questions.OrderByDescending(x => x.QuestionNumber))
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

