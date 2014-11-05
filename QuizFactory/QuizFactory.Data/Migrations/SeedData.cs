using System;
using System.Collections.Generic;
using System.Linq;
using QuizFactory.Models;

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
            this.Categories.Add(new Category() { Name = "Numbers" });
            this.Categories.Add(new Category() { Name = "Cats" });
            this.Categories.Add(new Category() { Name = "Christmas" });
            this.Categories.Add(new Category() { Name = "Nature" });
            this.Categories.Add(new Category() { Name = "Geography" });
            this.Categories.Add(new Category() { Name = "Music" });
            this.Categories.Add(new Category() { Name = "Movies" });

            ApplicationUser user = new ApplicationUser() { UserName = "gosho@abv.bg" };

            this.Quizzes = new List<QuizDefinition>();

            this.Quizzes.Add(new QuizDefinition()
            {
                Category = this.Categories[2],
                Title = "Christmas quiz",
                QuestionsDefinitions = this.GetQuestions1(),
                Author = user,
                CreationDate = DateTime.Now.AddDays(Rand.Next(-5, 5))
            });

            this.Quizzes.Add(new QuizDefinition()
            {
                Category = this.Categories[1],
                Title = "Got Bengal?",
                QuestionsDefinitions = this.GetQuestions2(),
                Author = user,
                CreationDate = DateTime.Now.AddDays(Rand.Next(-5, 5))
            });

            this.Quizzes.Add(new QuizDefinition()
            {
                Category = this.Categories[0],
                Title = "Numbers - part 1",
                QuestionsDefinitions = this.GetQuestions3(),
                Author = user,
                CreationDate = DateTime.Now.AddDays(Rand.Next(-5, 5))
            });

            this.Quizzes.Add(new QuizDefinition()
            {
                Category = this.Categories[0],
                Title = "Numbers - part 2",
                QuestionsDefinitions = this.GetQuestions4(),
                Author = user,
                CreationDate = DateTime.Now.AddDays(Rand.Next(-5, 5))
            });

            this.Quizzes.Add(new QuizDefinition()
            {
                Category = this.Categories[0],
                Title = "Numbers - part 3",
                QuestionsDefinitions = this.GetQuestions5(),
                Author = user,
                CreationDate = DateTime.Now.AddDays(Rand.Next(-5, 5))
            });
        }

        private List<QuestionDefinition> GetQuestions1()
        {
            List<QuestionDefinition> questions = new List<QuestionDefinition>
            {
                new QuestionDefinition()
                {
                    QuestionText = "In what country, the world's seventh largest by geographical area, is Christmas known as Bada Din (the big day)?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "India",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Cuba",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "China",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Christmas Island, in the Indian Ocean, is a territory of which country?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "India",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Cuba",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Australia",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "'Three Kings Day' is known by what numerical name (that's 'name', not 'date') in Britain?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Third Night",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "First Night",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Twelfth Night",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "The North Pole, said to be Santa's home, is located in which ocean?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Atlantic Ocean",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Arctic Ocean",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Indian Ocean",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "'And all the bells on earth shall ring, on Christmas day in the morning...' is from which Christmas carol?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "I Saw Three Ships",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "A Christmas Dinner",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "The Beauties of the Police",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Marzipan is made (conventionally in the western world) mainly from sugar and the flour or meal of which nut?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Peanut",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Almond",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Hazelnut",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Peter Auty sang Walking In The Air in what film?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "The Merry Widow",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "The Snowman",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Other",
                            IsCorrect = false
                        }
                    }
                },
            };

            return questions;
        }

        private List<QuestionDefinition> GetQuestions2()
        {
            List<QuestionDefinition> questions = new List<QuestionDefinition>
            {
                new QuestionDefinition()
                {
                    QuestionText = "The Bengal cat is a hybrid of a wildcat and a domestic cat. Which wildcat is used to create a Bengal?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Bengal tiger",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Clouded leopard",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Ocelot",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Asian leopard cat",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Several breeds of domestic cats have been used in creating Bengals. Which of the following breeds WOULD NOT be a good choice for creating a Bengal?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Abyssinian",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Persian",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Egyptian Mau",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "American Shorthair",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Jean Mill is usually credited with founding the Bengal cat breed. In 1963, she bred a wildcat with a domestic cat, with the resulting female offspring being fertile. Where did this breeding take place?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "New York",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Arizona",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "England",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Canada",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Bengal cats were created for their exotic spots, but what other coat pattern occurs?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Raccoon",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Himalayan",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Cloudy",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Marbled",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Which of the following is a coat trait that is unique to the Bengal breed?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Spots",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Curls",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Glitter",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Diamonds",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "In which of the following ways does a Bengal cat NOT differ from other domestic cats?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Body Length",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Claws",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Muscularity",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Head shape",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Your friend proudly displays her new Bengal kitten, but you suspect she has been duped. You turn the kitty over. What are you looking for?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "A spotted belly",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Stripes near the rear legs",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "A dark brown belly",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "'100% pure Bengal' tag",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Bengal cats are known for having doglike traits. Which of the following is NOT a Bengal trait?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Playing with water",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Drooling",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Following the owner",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Trainability",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Even the Bengal voice is different from other domestic cats. They make a variety of sounds to let you know exactly how they feel! But what sound are you UNLIKELY to hear from your Bengal?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "A loud yowl when they want attention",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Chattering",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Barking at strangers",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "A deep yowl before vomiting",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Some people might confuse Bengals with Ocicats. There are some physical differences, but what is the main difference between the two breeds?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Ocicats are mean",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "The Ocicat is a wildcat",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "The Ocicat was created from domestic cat breeds",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Ocicats have existed since the time of the Pharoahs",
                            IsCorrect = false
                        }
                    }
                }
            };

            return questions;
        }

        private List<QuestionDefinition> GetQuestions3()
        {
            List<QuestionDefinition> questions = new List<QuestionDefinition>
            {
                new QuestionDefinition()
                {
                    QuestionText = "Aside from an extra 385 yards, how many miles is a marathon race?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "27",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "19",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "25",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "26",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "If 27 solid cubes are formed into one big 3x3x3 cube how many individual cubes, at most, are visible from any single angle?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "18",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "19",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "10",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "16",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "In the movie Spinal Tap what number is: 'Well, it is one louder..'?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "12",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "11",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "9",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "101",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "'Via Dolorosa' is the (how many) Stations of the Cross, the Christian ritual tracing the key stages of the death of Jesus, beginning with his condemnation and ending with his being laid in the tomb?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "7",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "9",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "32",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "14",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "How many dots are on a (standard 1-6) dice?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "20",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "26",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "21",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "30",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "The Russian 'Crimea Highway' trunk road from Moscow to the Crimea in Ukraine is the M (what)?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "5",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "1",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "2",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "4",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "What number, between two hyphens, is used by journalists, etc., to mark the end of a newspaper or broadcast story?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "30",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "100",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "1",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "50",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "'Roaring' refers to what pluralised number in describing a 1900s decade of western world prosperity?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "30",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "20",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "90",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "50",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Traditionally what number of years anniversary is symbolized by silver?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "30",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "50",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "25",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "90",
                            IsCorrect = false
                        }
                    }
                }
            };

            return questions;
        }

        private List<QuestionDefinition> GetQuestions4()
        {
            List<QuestionDefinition> questions = new List<QuestionDefinition>
            {
                new QuestionDefinition()
                {
                    QuestionText = "How many unique dominoes are in a standard 'double six' set?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "27",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "30",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "25",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "28",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "What number turned on its side (rotated 90 degrees) is the symbol for infinity?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "0",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "8",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "101",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "3",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "The Marvel Comics superhero team led by Mr Fantastic was the Fanstastic (what)?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "Five",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Four",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "Six",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "Ten",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "What is the larger number of the binary system?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "0",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "2",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "1",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Japanese haiku poems loosely comprise how many syllables?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "11",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "13",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "17",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "21",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "The Tropics of Cancer and Capricorn are respectively (what number)-and-half degrees north and south of the Equator?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "45",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "30",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "23",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "20",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Greek deka, and Latin decem, are what number?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "10",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "100",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "1",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "50",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Conventionally how many books are in the Bible's New Testament?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "27",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "100",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "43",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "50",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "How many legs (or arms) are most usually on a starfish?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "7",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "4",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "2",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "5",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "A lunar month is an average (how many) days plus 12 hours, 44 minutes and 3 seconds?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "27",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "24",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "29",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "30",
                            IsCorrect = false
                        }
                    }
                }
            };

            return questions;
        }

        private List<QuestionDefinition> GetQuestions5()
        {
            List<QuestionDefinition> questions = new List<QuestionDefinition>
            {
                new QuestionDefinition()
                {
                    QuestionText = "What is generally stated to be the number of major joints in the human body?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "9",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "30",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "15",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "13",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "What is the only number that equals twice the sum of its digits (digit means numerical symbol)?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "22",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "18",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "11",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "54",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Any line of three numbers in the 'magic square' (a 3 x 3 grid of the numbers 1-9) adds up to what?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "13",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "15",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "21",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "20",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "What is the international SPI resin/polymer identification coding system number (typically shown within a recycling triangle symbol) for polystyrene?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "8",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "2",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "7",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "6",
                            IsCorrect = true
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Traditionally the diameter of the 45rpm gramophone record is (how many) inches?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "8",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "10",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "7",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "9",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Pure gold is (how many)-carat?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "18",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "21",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "24",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "20",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "The expression 'On cloud (what)' refers to being blissfully happy?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "9",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "8",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "1",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "10",
                            IsCorrect = false
                        }
                    }
                },
                new QuestionDefinition()
                {
                    QuestionText = "Each player begins with (how many) pieces in a game of chess?",
                    AnswersDefinitions = new List<AnswerDefinition>()
                    {
                        new AnswerDefinition()
                        {
                            Text = "16",
                            IsCorrect = true
                        },
                        new AnswerDefinition()
                        {
                            Text = "18",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "20",
                            IsCorrect = false
                        },
                        new AnswerDefinition()
                        {
                            Text = "14",
                            IsCorrect = false
                        }
                    }
                }
            };

            return questions;
        }
    }
}