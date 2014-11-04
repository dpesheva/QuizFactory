namespace QuizFactory.Data
{
    using System;
    using System.Linq;

    using QuizFactory.Data.Repositories;
    using QuizFactory.Models;

    // unit of work
    public interface IQuizFactoryData
    {
        IRepository<AnswerDefinition> AnswerDefinitions { get; }

        IRepository<QuestionDefinition> QuestionsDefinitions { get; }

        IRepository<QuizDefinition> QuizzesDefinitions { get; }

        IRepository<TakenQuiz> TakenQuizzes { get; }

        IRepository<UsersAnswer> UsersAnswers { get; }

        IRepository<Category> Categories { get; }

        //IRepository<User> Users { get; }

        void SaveChanges();
    }
}