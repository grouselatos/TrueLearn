using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;
using TrueLearn.Models;
using TrueLearn.ViewModels;

namespace TrueLearn.Controllers
{
	[Authorize]
    public class TodoTasksController : Controller
    {
		private DbManager db = new DbManager();

		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public JsonResult Events()
		{
			var events = db.GetTodoTasks().Where(x => x.UserId == User.Identity.GetUserId()).ToList();
			var result = events.Select(x => new
			{
				Id = x.Id,
				title = x.title,
				description = x.description,
				start = x.start_date,
				end = x.end_date
			});
			return Json(result, JsonRequestBehavior.AllowGet);
		}

		public ActionResult _Create()
		{
			TaskViewModel vm = new TaskViewModel()
			{
				TodoTask = new TodoTask(),
				Courses = db.GetCourses().Where(x => x.UserId == User.Identity.GetUserId()).Select(x => new SelectListItem()
				{
					Value = x.id.ToString(),
					Text = x.title
				})
			};
			return PartialView(vm);
		}

		//[HttpPut]
		//[ActionName("Event")]
		//public JsonResult AddEvent(TaskViewModel vm)
		//{
		//	vm.TodoTask.UserId = User.Identity.GetUserId();
		//	db.AddTodoTask(vm.TodoTask, vm.SelectedCourses);
		//	return Json(vm);
		//}

		[HttpPut]
		[ActionName("Event")]
		public JsonResult AddEvent(TodoTask todoTask)
		{
			todoTask.UserId = User.Identity.GetUserId();
			TaskViewModel vm = new TaskViewModel()
			{
				TodoTask = todoTask
			};
			bool result = db.AddTodoTaskBool(todoTask);
			return Json(result);
		}

		public ActionResult Edit(int id)
		{
			TodoTask todoTask = db.GetTodoTaskFull(id);
			if (todoTask == null)
			{
				return HttpNotFound();
			}
			TaskViewModel vm = new TaskViewModel()
			{
				TodoTask = todoTask,
				Courses = db.GetCourses().Where(x => x.UserId == User.Identity.GetUserId()).Select(x => new SelectListItem()
				{
					Value = x.id.ToString(),
					Text = x.title
				})
			};
			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(TaskViewModel vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			db.UpdateTodoTask(vm.TodoTask, vm.SelectedCourses);
			return RedirectToAction("Index", new { id = User.Identity.GetUserId() });
		}

		[HttpDelete]
		[ActionName("Event")]
		public JsonResult DeleteEvent(int id)
		{
			bool result = db.DeleteTodoTaskBool(id);
			return Json(result);
		}
	}
}