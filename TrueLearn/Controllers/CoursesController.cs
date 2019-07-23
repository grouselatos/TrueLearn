using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;
using TrueLearn.Models;

namespace TrueLearn.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private DbManager db = new DbManager();

        public ActionResult Index()
        {
            var courses = db.GetCourses().Where(x => x.UserId == User.Identity.GetUserId());
            return View(courses);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }
            course.UserId = User.Identity.GetUserId();
            db.AddCourse(course);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Course course = db.GetCourse(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Course course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }
            db.UpdateCourse(course);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Course course = db.GetCourse(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.DeleteCourse(id);
            return RedirectToAction("Index");
        }
    }
}