﻿namespace QuizFactory.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using HelperExtentions;
    using QuizFactory.Data.Common.Interfaces;

    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly IDbSet<T> set;

        public EFRepository(IQuizFactoryDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
            }

            this.Context = context;
            this.set = context.Set<T>();
        }

        protected IQuizFactoryDbContext Context { get; set; }

        public virtual T GetById(object id)
        {
            return this.set.Find(id);
        }

        public virtual IQueryable<T> All()
        {
            return this.set;
        }

        public virtual void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public T Find(object id)
        {
            return this.set.Find(id);
        }

        public virtual void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public virtual void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public virtual void Delete(object id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
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
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        /// <summary>
        /// This method updates database values by using expression. It works with both anonymous and class objects.
        /// It is used in one of the following ways:
        /// 1. .UpdateValues(x => new Type { Id = ..., Property = ..., AnotherProperty = ... })
        /// 2. .UpdateValues(x => new { Id = ..., Property = ..., AnotherProperty = ... })
        /// </summary>
        /// <param name="entity">Expression for the updated entity</param>
        public virtual void UpdateValues(Expression<Func<T, object>> entity)
        {
            // compile the expression to delegate and invoke it
            object compiledExpression = entity.Compile()(null);

            // cast the result of invokation to T
            T updatedEntity = compiledExpression is T ? compiledExpression as T : compiledExpression.CastTo<T>();

            // attach the entry if missing in ObjectStateManager
            var entry = this.Context.Entry(updatedEntity);

            if (entry.State == EntityState.Detached)
            {
                try
                {
                    this.set.Attach(updatedEntity);
                }
                catch
                {
                    var key = this.GetPrimaryKey(entry);
                    entry = this.Context.Entry(this.set.Find(key));
                    entry.CurrentValues.SetValues(updatedEntity);
                }
            }

            // get current database values of the entity
            var values = entry.GetDatabaseValues();
            if (values == null)
            {
                throw new InvalidOperationException("Object does not exists in ObjectStateDictionary. Entity Key|Id should be provided or valid.");
            }

            // select the updated members as property names
            IEnumerable<string> members;
            if (compiledExpression is T)
            {
                members = ((MemberInitExpression)entity.Body).Bindings.Select(b => b.Member.Name);
            }
            else
            {
                members = ((NewExpression)entity.Body).Members.Select(m => m.Name);
            }

            // select all not mapped properties and set value
            typeof(T)
                     .GetProperties()
                     .Where(pr => !pr.GetCustomAttributes(typeof(NotMappedAttribute), true).Any())
                     .ForEach(prop =>
                     {
                         if (members.Contains(prop.Name))
                         {
                             // if a member is updated set its state to modified
                             entry.Property(prop.Name).IsModified = true;
                         }
                         else
                         {
                             // otherwise set the existing database value
                             var value = values.GetValue<object>(prop.Name);
                             prop.SetValue(entry.Entity, value);
                         }
                     });
        }

        protected void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }

        private int GetPrimaryKey(DbEntityEntry entry)
        {
            var myObject = entry.Entity;

            var property = myObject
                                   .GetType()
                                   .GetProperties()
                                   .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(KeyAttribute)));

            return (int)property.GetValue(myObject, null);
        }
    }
}