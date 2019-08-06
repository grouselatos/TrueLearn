using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;
using TrueLearn.Models;
using TrueLearn.ViewModels;

namespace TrueLearn.Controllers
{
    public class PersonalChatController : Controller
    {
        private DbManager db = new DbManager();
        public ActionResult Chat(string friendId)
        {
            var friendship = db.GetFriendship(User.Identity.GetUserId(), friendId);
            var chat = db.GetPersonalChat(friendship.Id);
            PersonalChatViewModel vm = new PersonalChatViewModel
            {
                Chat = chat,
                User = db.GetUser(User.Identity.GetUserId()),
                Friend = db.GetUser(friendId),
                Messages = db.GetMessages(chat.Id),
                chatNotifications = db.GetChatNotifications(chat.Id)
            };
            return View(vm);
        }

        [HttpPut]
        public ActionResult AddMessage(string Text)
        {
            Message message = new Message
            {
                sent = DateTime.Now,
                sender = User.Identity.GetUserId(),
                text = Text
            };
            db.AddMessage(message);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}