namespace QuizFactory.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using QuizFactory.Data;
    using QuizFactory.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<QuizFactoryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(QuizFactoryDbContext context)
        {
            var seedData = new SeedData();
            if (!context.QuizDefinitions.Any())
            {
                foreach (var item in seedData.Quizzes)
                {
                    context.QuizDefinitions.Add(item);
                }
            }

            if (!context.Categories.Any())
            {
                foreach (var item in seedData.Categories)
                {
                    context.Categories.Add(item);
                }
            }

            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
