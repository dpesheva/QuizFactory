namespace QuizFactory.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using QuizFactory.Data.Models;

    public interface IQuizFactoryDbContext
    {
        IDbSet<AnswerDefinition> AnswerDefinitions { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<QuestionDefinition> QuestionDefinitions { get; set; }

        IDbSet<QuizDefinition> QuizDefinitions { get; set; }

        IDbSet<TakenQuiz> TakenQuizzes { get; set; }

        IDbSet<UsersAnswer> UsersAnswers { get; set; }

        IDbSet<Vote> Votes { get; set; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}