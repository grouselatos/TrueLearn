using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public class CourseTask
    {
        public int Id { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public int task { get; set; }
        public string description { get; set; }
        //public DateTime start_date { get; set; }
        //public DateTime end_date { get; set; }
        public virtual Course Course { get; set; }

    }
}