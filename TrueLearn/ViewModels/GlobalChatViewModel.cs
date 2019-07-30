using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrueLearn.Models;

namespace TrueLearn.ViewModels
{
    public class GlobalChatViewModel
    {
        public string UserName { get; set; }
        public DateTime Sent { get; set; }
        public string Message { get; set; }
    }
}