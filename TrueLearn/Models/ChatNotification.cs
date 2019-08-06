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
        public int Id { get; set; }

        [ForeignKey("Chat")]
        public int ChatId { get; set; }

        public int MessageId { get; set; }
        public string senderName { get;set; }
        public string receiver { get; set; }
        public virtual Chat Chat { get; set; }
    }
}