using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;

namespace TrueLearn.Controllers
{
    public class GlobalChatController : Controller
    {
        private DbManager db = new DbManager();
        // GET: GlobalChat
        public ActionResult Index()
        {
            var ChatHistory = db.GetChatHistory();
            return View();
        }
    }
}