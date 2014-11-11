namespace QuizFactory.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using QuizFactory.Data.Models;
    using QuizFactory.Data.Common.Interfaces;
    using QuizFactory.Data.Common.Repositories;


    public class QuizFactoryData : IQuizFactoryData
    {
        private QuizFactoryDbContext context;
        private IDictionary<Type, object> repositories;

        public static QuizFactoryData Create()
        {
            return new QuizFactoryData();
        }

        public QuizFactoryData(QuizFactoryDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public QuizFactoryData()
            : this(new QuizFactoryDbContext())
        {
        }

        public IDeletableEntityRepository<AnswerDefinition> AnswerDefinitions
        {
            get
            {
                return this.GetRepository<AnswerDefinition>();
            }
        }

        public IDeletableEntityRepository<QuestionDefinition> QuestionsDefinitions
        {
            get
            {
                return this.GetRepository<QuestionDefinition>();
            }
        }

        public IDeletableEntityRepository<QuizDefinition> QuizzesDefinitions
        {
            get
            {
                return this.GetRepository<QuizDefinition>();
            }
        }

        public IDeletableEntityRepository<TakenQuiz> TakenQuizzes
        {
            get
            {
                return this.GetRepository<TakenQuiz>();
            }
        }

        public IDeletableEntityRepository<UsersAnswer> UsersAnswers
        {
            get
            {
                return this.GetRepository<UsersAnswer>();
            }
        }

        public IDeletableEntityRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        public IDeletableEntityRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IDeletableEntityRepository<T> GetRepository<T>() where T : class, IDeletableEntity
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
