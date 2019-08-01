using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace TrueLearn.Hubs
{
    public class GlobalChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}