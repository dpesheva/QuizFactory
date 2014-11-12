﻿namespace QuizFactory.Data
{
    using System;
    using System.Linq;

    using QuizFactory.Data.Common.Interfaces;
    using QuizFactory.Data.Models;

    // unit of work
    public interface IQuizFactoryData
    {
        IDeletableEntityRepository <AnswerDefinition> AnswerDefinitions { get; }

        IDeletableEntityRepository<QuestionDefinition> QuestionsDefinitions { get; }
       
        IDeletableEntityRepository<QuizDefinition> QuizzesDefinitions { get; }
        
        IDeletableEntityRepository<TakenQuiz> TakenQuizzes { get; }
      
        IDeletableEntityRepository<UsersAnswer> UsersAnswers { get; }
      
        IDeletableEntityRepository<Category> Categories { get; }
      
        IDeletableEntityRepository<ApplicationUser> Users { get; }

        void SaveChanges();
    }
}