using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueLearn.Models;

namespace TrueLearn.ViewModels
{
    public class PersonalChatViewModel
    {
        public ApplicationUser User { get; set; }
        public ApplicationUser Friend { get; set; }
        public Chat Chat { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}