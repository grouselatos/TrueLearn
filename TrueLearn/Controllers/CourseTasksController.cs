using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;
using TrueLearn.Models;

namespace TrueLearn.Controllers
{
    public class CourseTasksController : Controller
    {
        private DbManager db = new DbManager();
        // GET: CourseTask
        public ActionResult Index(int id)
        {
            Session["id"] = id;
            var courseTasks = db.GetCourseTasks().Where(x => x.CourseId == id);
            return View(courseTasks);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseTask courseTask)
        {
            if (!ModelState.IsValid)
            {
                return View(courseTask);
            }
            var courseid = int.Parse(Session["id"].ToString()); 
            db.AddCourseTask(courseTask, courseid);
            return RedirectToAction("Index", new { id = courseid});
        }

        public ActionResult Edit(int id)
        {
            CourseTask courseTask = db.GetCourseTask(id);
            if (courseTask == null)
            {
                return HttpNotFound();
            }
            return View(courseTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseTask courseTask)
        {
            if (!ModelState.IsValid)
            {
                return View(courseTask);
            }
            db.UpdateCourseTask(courseTask);
            return RedirectToAction("Index", new { id = int.Parse(Session["id"].ToString()) });
        }

        public ActionResult Delete(int id)
        {
            CourseTask courseTask = db.GetCourseTask(id);
            if (courseTask == null)
            {
                return HttpNotFound();
            }
            return View(courseTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteCourseTask(id);
            return RedirectToAction("Index", new { id = int.Parse(Session["id"].ToString()) });
        }
    }
}