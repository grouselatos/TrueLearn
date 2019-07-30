using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrueLearn.Models;

namespace TrueLearn.Managers
{
    public class DbManager
    {
        public ICollection<Course> GetCourses()
        {
            ICollection<Course> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Courses.ToList();
            }
            return result;
        }

        public Course GetCourse(int id)
        {
            Course result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Courses.Find(id);
            }
            return result;
        }

        public void AddCourse(Course course)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                db.Courses.Add(course);
                db.SaveChanges();
            }
        }

        public void UpdateCourse(Course course)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Courses.Attach(course);
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCourse(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Course course = db.Courses.Find(id);
                db.Courses.Remove(course);
                db.SaveChanges();
            }
        }

        #region User

        public void UpgradeToPremium(string userId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                var result1 = userManager.RemoveFromRole(userId, "FreeUser");
                var result2 = userManager.AddToRole(userId, "PremiumUser");
            }
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            List<ApplicationUser> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Users.ToList();
            }
            return result;
        }

        public ICollection<ApplicationUser> GetUsers(string search)
        {
            List<ApplicationUser> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var query = db.Users.AsQueryable();
                if (!String.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.UserName.Contains(search) || x.Email.Contains(search) || ((x.first_name.ToString()) +(" ") + (x.last_name.ToString())).Contains(search));
                }
                result = query.ToList();
            }
            return result;
        }

        public ApplicationUser GetUser(string id)
        {
            ApplicationUser result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Users.Find(id);
            }
            return result;
        }

        public void AddFriend(Friend friend)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Friends.Add(friend);
                db.SaveChanges();
            }
        }

        public ICollection<ApplicationUser> GetNotifications(string userId)
        {
            ICollection<ApplicationUser> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var query = db.Users.Join(db.Friends,
                                        user => user.Id,
                                        friend => friend.senderId,
                                        (user, sender) => new { User = user, Sender = sender })
                                    .Where((x => x.User.Id == x.Sender.senderId && x.Sender.receiverId == userId && x.Sender.status == FriendStatus.Pending))
                                    .Select(x => x.User)
                                    .ToList();
                result = query;
            }
            return result;
        }

        public void AcceptRequest(string senderId, string receiverId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var friends = db.Friends.Where(x => x.senderId == senderId && x.receiverId == receiverId).FirstOrDefault();
                db.Friends.Attach(friends);
                friends.status = FriendStatus.Approved;
                db.Entry(friends).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public ICollection<ApplicationUser> GetFriends(string userId)
        {
            ICollection<ApplicationUser> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var query = db.Users.Join(db.Friends,
                                        user => user.Id,
                                        friend => friend.senderId,
                                        (user, sender) => new { User = user, Sender = sender })
                                    .Where((x => x.User.Id == x.Sender.senderId && x.Sender.receiverId == userId && x.Sender.status == FriendStatus.Approved))
                                    .Select(x => x.User)
                                    .ToList();
                result = query;
            }
            return result;
        }

        #endregion
    }
}