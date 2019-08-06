using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;
using TrueLearn.ViewModels;

namespace TrueLearn.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private DbManager db = new DbManager();

        // GET: Dashboard
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            DashboardViewModel vm = new DashboardViewModel()
            {
                User = db.GetUser(userId),
                friendNotifications = db.GetNotifications(userId),
                chatNotifications = db.GetChatNotifications(userId)
            };
            return View(vm);
        }
    }
}