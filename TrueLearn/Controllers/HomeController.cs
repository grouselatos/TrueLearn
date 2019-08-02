using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;
using TrueLearn.Models;
using TrueLearn.ViewModels;

namespace TrueLearn.Controllers
{
    public class HomeController : Controller
    {
        private DbManager db = new DbManager();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            
                var events = db.GetTodoTasks();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(TaskViewModel vm)
        {
            
            vm.TodoTask.UserId = User.Identity.GetUserId();
            db.AddTodoTask(vm.TodoTask, vm.SelectedCourses);
            return new JsonResult { Data = new { vm.TodoTask, vm.SelectedCourses } };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public JsonResult DeleteConfirmed(int id)
        {
            var status = false;
            var task = db.GetTodoTask(id);
            if (task != null)
            {
                db.DeleteTodoTask(id);
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}