namespace QuizFactory.Data
{
    using System;
    using System.Linq;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using QuizFactory.Data.Models;
    using QuizFactory.Data.Migrations;
    using QuizFactory.Data.Common.Interfaces;

    public class QuizFactoryDbContext : IdentityDbContext<ApplicationUser>
    {
        public QuizFactoryDbContext()
            : base("QuizDb", throwIfV1Schema: false)
        {
            Database.SetInitializer<QuizFactoryDbContext>(new MigrateDatabaseToLatestVersion<QuizFactoryDbContext, Configuration>());
        }

        public static QuizFactoryDbContext Create()
        {
            return new QuizFactoryDbContext();
        }

        public virtual DbSet<AnswerDefinition> AnswerDefinitions { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<QuestionDefinition> QuestionDefinitions { get; set; }
        public virtual DbSet<QuizDefinition> QuizDefinitions { get; set; }
        public virtual DbSet<TakenQuiz> TakenQuizzes { get; set; }
        public virtual DbSet<UsersAnswer> UsersAnswers { get; set; }

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
