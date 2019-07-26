using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TrueLearn.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            friends = new List<Friend>();
        }

        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime birth_date { get; set; }
        public string country { get; set; }
        public bool premium { get; set; }
        public virtual ICollection<Friend> friends { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
<<<<<<< HEAD
        public virtual DbSet<CourseTask> CourseTasks { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }
        //public virtual DbSet<Friend> Friends { get; set; }
        //public virtual DbSet<Premium_User> Premium_Users { get; set; }
        //public virtual DbSet<Tracked_Course> Tracked_Courses { get; set; }
=======
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Premium_User> Premium_Users { get; set; }
        public virtual DbSet<Tracked_Course> Tracked_Courses { get; set; }
>>>>>>> f34153fc6334afa32f8c88630609fbc8ff889870
    }
}