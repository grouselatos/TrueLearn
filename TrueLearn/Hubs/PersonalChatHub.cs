using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TrueLearn.Managers;
using TrueLearn.Models;

namespace TrueLearn.Hubs
{
    [HubName("PersonalChatHub")]
    public class PersonalChatHub : Hub
    {
        private DbManager db = new DbManager();
        public void SendToUser(int chatId, string to, string message)
        {
            Message newMessage = new Message
            {
                sent = DateTime.Now,
                ChatId = chatId,
                sender = Context.User.Identity.Name,
                text = message
            };
            db.AddMessage(newMessage);
            Clients.User(to).gotMessage(Context.User.Identity.Name, message);
        }
    }
}