using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public enum FriendStatus
    {
        Pending,
        Approved
    }
    public class Friend
    {
        public int Id { get; set; }

        [ForeignKey("Sender")]
        public string senderId { get; set; }
        
        [ForeignKey("Receiver")]
        public string receiverId { get; set; }

        public FriendStatus status { get; set; }
        public DateTime triggered { get; set; }

        public virtual Chat Chat { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public virtual ApplicationUser Receiver { get; set; }
        public virtual ICollection<FriendNotification> FriendNotifications { get; set; }
    }
}