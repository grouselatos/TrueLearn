using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrueLearn.Managers;
using TrueLearn.Models;
using TrueLearn.ViewModels;

namespace TrueLearn.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private DbManager db = new DbManager();
        // GET: Friends
        public ActionResult Index(string search)
        {
            FriendsViewModel vm = new FriendsViewModel()
            {
                Friends = db.GetUsers(search),
                Search = search
            };
            return View(vm);
        }

        public ActionResult AddFriend(string id)
        {
            Session["FriendId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFriend()
        {
            Friend friend = new Friend()
            {
                senderId = User.Identity.GetUserId(),
                receiverId = Session["FriendId"].ToString(),
                status = FriendStatus.Pending,
                triggered = DateTime.Now
            };
            db.AddFriend(friend);
            return RedirectToAction("Index");
        }

        public ActionResult Notifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = db.GetNotifications(userId);
            return View(notifications);
        }

        [HttpPost]
        public ActionResult AcceptRequest(string senderId)
        {
            db.AcceptRequest(senderId, User.Identity.GetUserId());
            return new EmptyResult();
        }

        public ActionResult FriendList()
        {
            ICollection<ApplicationUser> friends;
            friends = db.GetFriends(User.Identity.GetUserId());
            return View(friends);
        }
    }
}


