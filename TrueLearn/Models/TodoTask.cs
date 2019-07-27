using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public class TodoTask
    {
        public int Id { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}