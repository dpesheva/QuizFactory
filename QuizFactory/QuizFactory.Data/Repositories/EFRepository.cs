﻿using System;
using System.Linq;

namespace QuizFactory.Data.Repositories
{
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class EFRepository<T> : IRepository<T> where T : class
    {
        private QuizFactoryDbContext context;
        private IDbSet<T> set;

        public EFRepository()
            : this(new QuizFactoryDbContext())
        {
        }

        public EFRepository(QuizFactoryDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public IQueryable<T> All()
        {
            return this.set;
        }

        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public T Find(object id)
        {
            return this.set.Find(id);
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public T Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
            return entity;
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return this.set.Where(predicate);
        }

        public void Detach(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Detached);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        private void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }
            entry.State = state;
        }
    }
}