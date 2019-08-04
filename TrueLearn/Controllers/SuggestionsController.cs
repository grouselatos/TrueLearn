using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;
using TrueLearn.Models;

namespace TrueLearn.Controllers
{
    [CustomAuthorize(Roles = "PremiumUser")]
    public class SuggestionsController : Controller
    {
        private DbManager db = new DbManager();

        public ActionResult Index()
        {
            return View();
        }
    }
}