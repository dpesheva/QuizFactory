namespace QuizFactory.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using QuizFactory.Data.Common.Interfaces;

    public class DeletableEntityRepository<T> : EFRepository<T>, IDeletableEntityRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(IQuizFactoryDbContext context)
            : base(context)
        {
        }
       
        public override void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
            base.ChangeEntityState(entity, EntityState.Modified);
        }

        public void ActualDelete(T entity)
        {
            base.Delete(entity);
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }
    }
}