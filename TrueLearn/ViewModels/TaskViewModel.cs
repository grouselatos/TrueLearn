using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Models;

namespace TrueLearn.ViewModels
{
	public class TaskViewModel
	{
		public TodoTask TodoTask { get; set; }
		public IEnumerable<SelectListItem> Courses { get; set; }

		private List<int> _selectedCourses;

		public List<int> SelectedCourses
		{
			get
			{
				if (_selectedCourses == null)
				{
					_selectedCourses = TodoTask.Courses.Select(x => x.id).ToList();
				}
				return _selectedCourses;
			}
			set
			{
				_selectedCourses = value;
			}
		}
	}
}