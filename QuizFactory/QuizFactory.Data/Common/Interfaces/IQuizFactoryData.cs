namespace QuizFactory.Data
{
    using System;
    using System.Linq;

    using QuizFactory.Data.Common.Interfaces;
    using QuizFactory.Data.Models;

    // unit of work
    public interface IQuizFactoryData
    {
        IRepository<AnswerDefinition> AnswerDefinitions { get; }

        IRepository<QuestionDefinition> QuestionsDefinitions { get; }

        IRepository<QuizDefinition> QuizzesDefinitions { get; }

        IRepository<TakenQuiz> TakenQuizzes { get; }

        IRepository<UsersAnswer> UsersAnswers { get; }

        IRepository<Category> Categories { get; }

        IRepository<ApplicationUser> Users { get; }

        void SaveChanges();
    }
}