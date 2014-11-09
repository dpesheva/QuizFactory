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
            if (!context.Roles.Any())
            {
               var seedUsers = new SeedUsers();
               seedUsers.Generate(context);
            }
            
            var seedData = new SeedData(context);
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
        }
    }
}
