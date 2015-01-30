namespace QuizFactory.Data.Migrations
{
    using System;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using QuizFactory.Data.Common;
    using QuizFactory.Data.Models;

    public class SeedUsers
    {
        public void Generate(QuizFactoryDbContext context)
        {
            //this.CreateUserAndRole(context, "pesho@abv.bg", GlobalConstants.AdminRole);
            this.CreateUserAndRole(context, "gosho@abv.bg", GlobalConstants.UserRole);
        }

        private void CreateUserAndRole(QuizFactoryDbContext context, string username, string rolename)
        {
            var existingUser = context.Users.FirstOrDefault(x => x.UserName == username);
            if (existingUser == null)
            {
                this.CreateUser(context, username, rolename);
            }
        }

        private void CreateUser(QuizFactoryDbContext context, string userName, string roleName)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user = new ApplicationUser() { UserName = userName, Email = userName };

            // the password is equal to userName
            userManager.Create(user, userName);

            this.AddToRole(context, roleName, user);
        }

        private void AddToRole(QuizFactoryDbContext context, string roleName, ApplicationUser user)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            roleManager.Create(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = roleName });

            userManager.AddToRole(user.Id, roleName);
        }
    }
}