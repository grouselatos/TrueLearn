using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public class ChatNotification
    {
        [Key, ForeignKey("Message")]
        public int ChatId { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
        public DateTime triggered { get; set; }
        public virtual Message Message { get; set; }
    }
}