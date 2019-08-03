using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TrueLearn.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        

        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime birth_date { get; set; }
        public string country { get; set; }
        [NotMapped]
        public HttpPostedFileBase ProfileImageFile { get; set; }

        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<TodoTask> TodoTasks { get; set; }
        public virtual ICollection<GlobalChat> GlobalChats { get; set; }

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
        public virtual DbSet<TodoTask> TodoTasks { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<FriendNotification> FriendNotifications { get; set; }
        public virtual DbSet<ChatNotification> ChatNotifications { get; set; }
        public virtual DbSet<GlobalChat> GlobalChats { get; set; }
    }
}