using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueLearn.Models;

namespace TrueLearn.ViewModels
{
    public class FriendsNotificationViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Friend> Friends { get; set; }
    }
}