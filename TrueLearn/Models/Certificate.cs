using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TrueLearn.Models
{
    //[Authorize(Roles="Admin, PremiumUser")]
    public class Certificate
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [DisplayName("Upload File")]
        public string CertificatePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase CertificateFile { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}