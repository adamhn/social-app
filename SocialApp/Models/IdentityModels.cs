using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SocialApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(55)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(55)]
        public string Lastname { get; set; }

        [StringLength(55)]
        public string Work { get; set; }

        [StringLength(55)]
        public string Study { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public RelationshipStatus RelationshipStatus { get; set; }

        public byte RelationshipStatusId { get; set; }

        public byte[] Picture { get; set; }

        public bool IsHiddenFromSearch { get; set; }

        public ApplicationUser()
        {
            IsHiddenFromSearch = false;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string GetFullName()
        {
            return Firstname + " " + Lastname;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<RelationshipStatus> RelationshipStatus { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}