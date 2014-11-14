namespace QuizFactory.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using QuizFactory.Data.Common.Interfaces;
    using QuizFactory.Data.Migrations;
    using QuizFactory.Data.Models;

    public class QuizFactoryDbContext : IdentityDbContext<ApplicationUser>, IQuizFactoryDbContext
    {
        public QuizFactoryDbContext()
            : base("QuizDb", throwIfV1Schema: false)
        {
            Database.SetInitializer<QuizFactoryDbContext>(new MigrateDatabaseToLatestVersion<QuizFactoryDbContext, Configuration>());
        }
         
        public virtual IDbSet<AnswerDefinition> AnswerDefinitions { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<QuestionDefinition> QuestionDefinitions { get; set; }

        public virtual IDbSet<QuizDefinition> QuizDefinitions { get; set; }

        public virtual IDbSet<TakenQuiz> TakenQuizzes { get; set; }

        public virtual IDbSet<UsersAnswer> UsersAnswers { get; set; }

        public static QuizFactoryDbContext Create()
        {
            return new QuizFactoryDbContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
       
        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }
                 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TakenQuiz>()
                        .HasMany(e => e.UsersAnswers)
                        .WithRequired(e => e.TakenQuiz)
            // .HasForeignKey(e => e.TakenQuizId)
                        .WillCascadeOnDelete(false);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                         e =>
                             e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}