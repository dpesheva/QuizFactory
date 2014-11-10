namespace QuizFactory.Data.Common.Repositories
{
    using System.Linq;

    using System.Data.Entity;
    using QuizFactory.Data.Common.Interfaces;
    using System;

    public class DeletableEntityRepository<T> : EFRepository<T>, IDeletableEntityRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(DbContext context)
            : base(context)
        {
        }

        public override void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;
            base.ChangeEntityState(entity, EntityState.Modified);
        }

        public void HardDelete(T entity)
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