using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueLearn.Models;

namespace TrueLearn.ViewModels
{
    public class FriendsViewModel
    {
        public IEnumerable<ApplicationUser> Friends { get; set; }
        public string Search { get; set; }
    }
}