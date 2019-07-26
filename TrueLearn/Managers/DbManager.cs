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

        #region CourseTasks

        public ICollection<CourseTask> GetCourseTasks()
        {
            ICollection<CourseTask> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.CourseTasks.ToList();
            }
            return result;
        }

        public void AddCourseTask(CourseTask courseTask, int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                courseTask.CourseId = id;
                db.CourseTasks.Add(courseTask);
                db.SaveChanges();
            }
        }

        public CourseTask GetCourseTask(int id)
        {
            CourseTask result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.CourseTasks.Find(id);
            }
            return result;
        }

        public void UpdateCourseTask(CourseTask courseTask)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.CourseTasks.Attach(courseTask);
                db.Entry(courseTask).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCourseTask(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                CourseTask courseTask = db.CourseTasks.Find(id);
                db.CourseTasks.Remove(courseTask);
                db.SaveChanges();
            }
        }

        #endregion
    }
}