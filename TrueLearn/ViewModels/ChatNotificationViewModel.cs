using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueLearn.Models;

namespace TrueLearn.ViewModels
{
    public class ChatNotificationViewModel
    {
        public string friend { get; set; }
        public string senderName { get; set; }
        public string text { get; set; }
        public DateTime timeSent { get; set; }
        public int chatId { get; set; }
    }
}