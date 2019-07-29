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
    [Authorize(Roles = "PremiumUser")]
    public class CertificatesController : Controller
    {
        private DbManager db = new DbManager();

        public ActionResult Index()
        {
            var certificates = db.GetCertificates().Where(x => x.UserId == User.Identity.GetUserId());
            return View(certificates);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return View(certificate);
            }
            certificate.UserId = User.Identity.GetUserId();
            db.AddCertificate(certificate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Certificate certificate = db.GetCertificate(Id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit (Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return View(certificate);
            }
            db.UpdateCertificate(certificate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            Certificate certificate = db.GetCertificate(Id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int Id)
        {
            db.DeleteCourse(Id);
            return RedirectToAction("Index");
        }
    }
}