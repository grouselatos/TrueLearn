using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public class Course
    {
        [Key]
        [Required]
        public int id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public bool started { get; set; }
        public bool completed { get; set; }
        public bool tracked { get; set; }
        public virtual ICollection<CourseTask> CourseTasks { get; set; }
    }
}