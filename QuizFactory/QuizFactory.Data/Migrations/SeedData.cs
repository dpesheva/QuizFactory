using QuizFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizFactory.Migrations
{
    public class SeedData
    {
        public static Random Rand = new Random();

        public List<Category> Categories;

        public List<QuizDefinition> Quizzes;

        public SeedData()
        {
            this.Categories = new List<Category>();
            Categories.Add(new Category() { Name = "Numbers" });
            Categories.Add(new Category() { Name = "Cats" });
            Categories.Add(new Category() { Name = "Christmas" });

            ApplicationUser user = new ApplicationUser() { UserName = "Pesho" };


            this.Quizzes = new List<QuizDefinition>();


            Quizzes.Add(new QuizDefinition()
            {
                Category = Categories[2],
                Title = "Cristmas quiz",
                QuestionsDefinitions = GetQuestions1(),
                Author = user,
                CreationDate = DateTime.Now.AddDays(Rand.Next(-5, 5))
            });
        }

        private List<QuestionDefinition> GetQuestions1()
        {
            List<QuestionDefinition> questions = new List<QuestionDefinition>{
         new QuestionDefinition(){
             QuestionText="In what country, the world's seventh largest by geographical area, is Christmas known as Bada Din (the big day)?",
             AnswersDefinitions = new List<AnswerDefinition>(){
                 new AnswerDefinition(){
                     Text="India",
                     IsCorrect=true
                 },
                  new AnswerDefinition(){
                     Text="Cuba",
                     IsCorrect=false
                 },
                  new AnswerDefinition(){
                     Text="China",
                     IsCorrect=false
                 }
             }
         },
            new QuestionDefinition(){
             QuestionText="Christmas Island, in the Indian Ocean, is a territory of which country?",
             AnswersDefinitions = new List<AnswerDefinition>(){
                 new AnswerDefinition(){
                     Text="India",
                     IsCorrect=false
                 },
                  new AnswerDefinition(){
                     Text="Cuba",
                     IsCorrect=false
                 },
                  new AnswerDefinition(){
                     Text="Australia",
                     IsCorrect=true
                 }
          }
            },
             new QuestionDefinition(){
             QuestionText="'Three Kings Day' is known by what numerical name (that's 'name', not 'date') in Britain?",
             AnswersDefinitions = new List<AnswerDefinition>(){
                 new AnswerDefinition(){
                     Text="Third Night",
                     IsCorrect=false
                 },
                  new AnswerDefinition(){
                     Text="First Night",
                     IsCorrect=false
                 },
                  new AnswerDefinition(){
                     Text="Twelfth Night",
                     IsCorrect=true
                 }
          }
         },
           new QuestionDefinition(){
             QuestionText="The North Pole, said to be Santa's home, is located in which ocean?",
             AnswersDefinitions = new List<AnswerDefinition>(){
                 new AnswerDefinition(){
                     Text="Atlantic Ocean",
                     IsCorrect=false
                 },
                  new AnswerDefinition(){
                     Text="Arctic Ocean",
                     IsCorrect=true
                 },
                  new AnswerDefinition(){
                     Text="Indian Ocean",
                     IsCorrect=false
                 }
          }
         },
         new QuestionDefinition(){
             QuestionText="'And all the bells on earth shall ring, on Christmas day in the morning...' is from which Christmas carol?",
             AnswersDefinitions = new List<AnswerDefinition>(){
                 new AnswerDefinition(){
                     Text="I Saw Three Ships",
                     IsCorrect=true
                 },
                  new AnswerDefinition(){
                     Text="A Christmas Dinner",
                     IsCorrect=false
                 },
                  new AnswerDefinition(){
                     Text="The Beauties of the Police",
                     IsCorrect=false
                 }
          }
         },
            new QuestionDefinition(){
             QuestionText="Marzipan is made (conventionally in the western world) mainly from sugar and the flour or meal of which nut?",
             AnswersDefinitions = new List<AnswerDefinition>(){
                 new AnswerDefinition(){
                     Text="Peanut",
                     IsCorrect=false
                 },
                  new AnswerDefinition(){
                     Text="Almond",
                     IsCorrect=true
                 },
                  new AnswerDefinition(){
                     Text="Hazelnut",
                     IsCorrect=false
                 }
          }
         },
          new QuestionDefinition(){
             QuestionText="Peter Auty sang Walking In The Air in what film?",
             AnswersDefinitions = new List<AnswerDefinition>(){
                 new AnswerDefinition(){
                     Text="The Merry Widow",
                     IsCorrect=false
                 },
                  new AnswerDefinition(){
                     Text="The Snowman",
                     IsCorrect=true
                 },
                  new AnswerDefinition(){
                     Text="Other",
                     IsCorrect=false
                 }
          }
         },
         };

            return questions;
        }


    }
}