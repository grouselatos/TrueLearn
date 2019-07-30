using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public enum CourseStatus
    {
        NotStarted,
        InProgress,
        Completed
    }
    public enum CourseProvider
    {
        Udemy,
        Edx,
        Udacity,
        Coursera,
        KhanAcademy,
        Skillshare,
        Futurelearn
    }
    public class Course
    {
        [Key]
        [Required]
        public int id { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public string headline { get; set; }
        public string description { get; set; }
        public byte[] image { get; set; }
        public CourseProvider provider { get; set; }
        public string url { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string title { get; set; }
        public string category { get; set; }
        public CourseStatus status { get; set; }
        public bool tracked { get; set; }
        public virtual ICollection<TodoTask> TodoTasks { get; set; }
    }
}