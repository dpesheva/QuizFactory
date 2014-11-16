namespace QuizFactory.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using QuizFactory.Data.Common.Interfaces;
    using QuizFactory.Data.Models;
    using QuizFactory.Data.Repositories;

    public class QuizFactoryData : IQuizFactoryData
    {
        private readonly IQuizFactoryDbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public QuizFactoryData(IQuizFactoryDbContext context)
        {
            this.context = context;
        }

        //public QuizFactoryData()
        //    : this(new QuizFactoryDbContext())
        //{
        //}
        public IQuizFactoryDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IDeletableEntityRepository<AnswerDefinition> AnswerDefinitions
        {
            get
            {
                return this.GetDeletableEntityRepository<AnswerDefinition>();
            }
        }

        public IDeletableEntityRepository<QuestionDefinition> QuestionsDefinitions
        {
            get
            {
                return this.GetDeletableEntityRepository<QuestionDefinition>();
            }
        }

        public IDeletableEntityRepository<QuizDefinition> QuizzesDefinitions
        {
            get
            {
                return this.GetDeletableEntityRepository<QuizDefinition>();
            }
        }

        public IDeletableEntityRepository<TakenQuiz> TakenQuizzes
        {
            get
            {
                return this.GetDeletableEntityRepository<TakenQuiz>();
            }
        }

        public IDeletableEntityRepository<UsersAnswer> UsersAnswers
        {
            get
            {
                return this.GetDeletableEntityRepository<UsersAnswer>();
            }
        }

        public IDeletableEntityRepository<Category> Categories
        {
            get
            {
                return this.GetDeletableEntityRepository<Category>();
            }
        }

        public IDeletableEntityRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetDeletableEntityRepository<ApplicationUser>();
            }
        }

        public IDeletableEntityRepository<Vote> Votes
        {
            get
            {
                return this.GetDeletableEntityRepository<Vote>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                this.repositories.Add(typeOfModel, Activator.CreateInstance(typeof(DeletableEntityRepository<T>), this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeOfModel];
        }
    }
}