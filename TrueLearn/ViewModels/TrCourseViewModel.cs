using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueLearn.Models;

namespace TrueLearn.ViewModels
{
    public class TrCourseViewModel
    {
        public Course course { get; set; }

        public ICollection<CourseTask> courseTask { get; set; }

        private ICollection<CourseTask> tasks;
        public ICollection<CourseTask> Tasks {
            get
            {
                if (tasks == null)
                {
                    tasks = course.CourseTasks.Where(x => x.CourseId == course.id).ToList();
                }
                return tasks;
            }

            set
            {
                tasks = value;
            }
            
        }

    }
}