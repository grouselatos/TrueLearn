using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Models;
using TrueLearn.Managers;
using Microsoft.AspNet.Identity;

namespace TrueLearn.Controllers
{
    public class ChatNotificationsController : Controller
    {
        private DbManager db = new DbManager();

        public ActionResult Index()
        {
            var chatNotifications = db.GetChatNotifications(User.Identity.GetUserId());
            return View(chatNotifications);
        }
        
    }
}