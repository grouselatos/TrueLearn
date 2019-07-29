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
			var todoTasks = db.GetTodoTasks().Where(x => x.UserId == User.Identity.GetUserId()).ToList();
			return View(todoTasks);
		}

		public ActionResult Create()
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
			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(TaskViewModel vm)
		{
			if (!ModelState.IsValid)
			{
				return View(vm);
			}
			vm.TodoTask.UserId = User.Identity.GetUserId();
			db.AddTodoTask(vm.TodoTask, vm.SelectedCourses);
			return RedirectToAction("Index", new { id = User.Identity.GetUserId() });
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

		public ActionResult Delete(int id)
		{
			TodoTask todoTask = db.GetTodoTaskFull(id);
			if (todoTask == null)
			{
				return HttpNotFound();
			}
			return View(todoTask);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			db.DeleteTodoTask(id);
			return RedirectToAction("Index", new { id = User.Identity.GetUserId() });
		}
	}
}