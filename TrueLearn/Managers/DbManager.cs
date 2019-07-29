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

		#endregion

		#region TodoTasks

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

		#endregion
	}
}