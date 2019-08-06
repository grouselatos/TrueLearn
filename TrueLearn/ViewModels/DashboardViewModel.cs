using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueLearn.Models;
using TrueLearn.ViewModels;

namespace TrueLearn.ViewModels
{
	public class DashboardViewModel
	{
        public ApplicationUser User { get; set; }
        public IEnumerable<ChatNotificationViewModel> chatNotifications { get; set; }

        public IEnumerable<ApplicationUser> friendNotifications { get; set; }
	}
}