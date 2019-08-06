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

    }
}