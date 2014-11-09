namespace QuizFactory.Data.Common.Repositories
{
    using System.Linq;

    using System.Data.Entity;
    using QuizFactory.Data.Common.Interfaces;

    public class DeletableEntityRepository<T> : EFRepository<T>, IDeletableEntityRepository<T>
        where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(QuizFactoryDbContext context)
            : base(context)
        {
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