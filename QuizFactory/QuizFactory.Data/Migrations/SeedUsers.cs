using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QuizFactory.Data.Models;

namespace QuizFactory.Data.Migrations
{
    public class SeedUsers
    {
        public void Generate(QuizFactoryDbContext context)
        {
           CreateUserAndRole(context, "admin@abv.bg", "admin");
           CreateUserAndRole(context, "gosho@abv.bg", "user");

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