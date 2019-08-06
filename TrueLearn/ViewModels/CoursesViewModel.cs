using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueLearn.Models;

namespace TrueLearn.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
        public CourseStatus Status { get; set; }
    }
}