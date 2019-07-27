using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public class FriendNotification
    {
        public int id { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public DateTime triggered { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
    }
}