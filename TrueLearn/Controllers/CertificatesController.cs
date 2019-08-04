using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;
using TrueLearn.Models;

namespace TrueLearn.Controllers
{
    [CustomAuthorize(Roles = "PremiumUser")]
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
            string certificatefilename = Path.GetFileNameWithoutExtension(certificate.CertificateFile.FileName);
            string certificatefileextension = Path.GetExtension(certificate.CertificateFile.FileName);
            certificatefilename = Guid.NewGuid() + certificatefileextension;
            certificate.CertificatePath = "~/Images/" + certificatefilename;
            certificatefilename = Path.Combine(Server.MapPath("~/Images/"), certificatefilename);
            certificate.CertificateFile.SaveAs(certificatefilename);
            db.AddCertificate(certificate);
            ModelState.Clear();
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
        public ActionResult DeleteConfirmed(Certificate certificate, int id)
        {
            certificate.UserId = User.Identity.GetUserId();
            string certificatefilename = db.GetCertificate(id).CertificatePath;
            string certificatefilepath = Server.MapPath(certificatefilename);
            FileInfo file = new FileInfo(certificatefilepath);
            if (file.Exists)
            {
                file.Delete();
            }
            db.DeleteCertificate(id);
            return RedirectToAction("Index");
        }
    }
}