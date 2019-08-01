using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrueLearn.Models
{
    public class Message
    {
        public int Id { get; set; }

        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        public string sender { get; set; }
        public DateTime sent { get; set; }
        public string text { get; set; }
        public virtual ChatNotification ChatNotification { get; set; }
        public virtual Chat Chat { get; set; }

    }
}