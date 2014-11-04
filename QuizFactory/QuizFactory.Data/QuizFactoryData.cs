namespace QuizFactory.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using QuizFactory.Data.Repositories;
    using QuizFactory.Models;


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

        public IRepository<AnswerDefinition> AnswerDefinitions
        {
            get
            {
                return this.GetRepository<AnswerDefinition>();
            }
        }

        public IRepository<QuestionDefinition> QuestionsDefinitions
        {
            get
            {
                return this.GetRepository<QuestionDefinition>();
            }
        }

        public IRepository<QuizDefinition> QuizzesDefinitions
        {
            get
            {
                return this.GetRepository<QuizDefinition>();
            }
        }

        public IRepository<TakenQuiz> TakenQuizzes
        {
            get
            {
                return this.GetRepository<TakenQuiz>();
            }
        }

        public IRepository<UsersAnswer> UsersAnswers
        {
            get
            {
                return this.GetRepository<UsersAnswer>();
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                return this.GetRepository<Category>();
            }
        }

        //public IRepository<ApplicationUser> Users
        //{
        //    get
        //    {
        //        return this.GetRepository<ApplicationUser>();
        //    }
        //}

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                this.repositories.Add(typeOfModel, Activator.CreateInstance(typeof(EFRepository<T>), this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
