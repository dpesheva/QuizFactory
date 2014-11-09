namespace QuizFactory.Data.Models
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using QuizFactory.Data.Common.Interfaces;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.CreatedOn = DateTime.Now;
        }

        public ClaimsIdentity GenerateUserIdentity(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            return Task.FromResult(GenerateUserIdentity(manager));
        }

        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }
        
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}