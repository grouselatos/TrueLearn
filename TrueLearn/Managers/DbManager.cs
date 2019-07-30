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
        #region Course
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
        #endregion

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

		#endregion

        #region Certificate
        public ICollection<Certificate> GetCertificates()
        {
            ICollection<Certificate> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Certificates.ToList();
            }
            return result;
        }

		public ICollection<TodoTask> GetTodoTasks()
		{
			ICollection<TodoTask> result;
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				result = db.TodoTasks.Include("Courses")
									 .ToList();
			}
			return result;
		}
        public Certificate GetCertificate(int Id)
        {
            Certificate result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Certificates.Find(Id);
            }
            return result;
        }

        public void AddCertificate(Certificate certificate)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Certificates.Add(certificate);
                db.SaveChanges();
            }
        }

        public void UpdateCertificate(Certificate certificate)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Certificates.Attach(certificate);
                db.Entry(certificate).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCertificate(int Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Certificate certificate = db.Certificates.Find(Id);
                db.Certificates.Remove(certificate);
                db.SaveChanges();
            }
        }
        #endregion

        public ICollection<ApplicationUser> GetUsers()
        {
            List<ApplicationUser> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Users.ToList();
            }
            return result;
        }
		public void AddTodoTask(TodoTask todoTask, List<int> courseIds)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				db.TodoTasks.Add(todoTask);
				db.SaveChanges();
				foreach (int id in courseIds)
				{
					Course course = db.Courses.Find(id);
					if (course != null)
					{
						todoTask.Courses.Add(course);
					}
				}
				db.SaveChanges();
			}
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
		public TodoTask GetTodoTask(int id)
		{
			TodoTask result;
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				result = db.TodoTasks.Find(id);
			}
			return result;
		}

		public TodoTask GetTodoTaskFull(int id)
		{
			TodoTask result;
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				result = db.TodoTasks.Include("Courses")
									 .Where(x => x.Id == id)
									 .FirstOrDefault();
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
		public void UpdateTodoTask(TodoTask todoTask, List <int> courseIds)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				db.TodoTasks.Attach(todoTask);
				db.Entry(todoTask).Collection("Courses").Load();
				todoTask.Courses.Clear();
				db.SaveChanges();
				foreach (int id in courseIds)
				{
					Course course = db.Courses.Find(id);
					if (course != null)
					{
						todoTask.Courses.Add(course);
					}
				}
				db.Entry(todoTask).State = EntityState.Modified;
				db.SaveChanges();
			}
		}

		public void DeleteTodoTask(int id)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				TodoTask todoTask = db.TodoTasks.Find(id);
				db.TodoTasks.Remove(todoTask);
				db.SaveChanges();
			}
		}
        public void AddFriend(Friend friend)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Friends.Add(friend);
                db.SaveChanges();
            }
        }

        public ICollection<Friend> GetNotifications(string userId)
        {
            ICollection<Friend> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Friends.Where(x => (x.receiverId == userId) && (x.status == FriendStatus.Pending)).ToList();
            }
            return result;
        }
    }
		#endregion
	}
}