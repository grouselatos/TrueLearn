using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public class GlobalChat
    {
        public int id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public DateTime sent { get; set; }
        public string message { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}