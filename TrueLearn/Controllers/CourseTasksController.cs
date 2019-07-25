using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;

namespace TrueLearn.Controllers
{
    public class CourseTasksController : Controller
    {
		private DbManager db = new DbManager();
        // GET: CourseTasks
        public ActionResult Index(int id)
        {
			var courseTasks = db.GetCourseTasks().Where(x => x.CourseId == id);
			return View(courseTasks);
        }
    }
}