﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrueLearn.Models;
using TrueLearn.ViewModels;

namespace TrueLearn.Managers
{
    public class DbManager
    {
        #region Course
        public ICollection<Course> GetCourses()
        {
            ICollection<Course> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Courses.ToList();
            }
            return result;
        }

        public Course GetCourse(int id)
        {
            Course result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Courses.Find(id);
            }
            return result;
        }

        public void AddCourse(Course course)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Courses.Add(course);
                db.SaveChanges();
            }
        }

        public void UpdateCourse(Course course)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Courses.Attach(course);
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCourse(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Course course = db.Courses.Find(id);
                db.Courses.Remove(course);
                db.SaveChanges();
            }
        }
        #endregion

        #region User

        public void UpgradeToPremium(string userId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                var result1 = userManager.RemoveFromRole(userId, "FreeUser");
                var result2 = userManager.AddToRole(userId, "PremiumUser");
            }
        }

        public ApplicationUser GetUser(string id)
        {
            ApplicationUser result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Users.Find(id);
            }
            return result;
        }

        public ICollection<ApplicationUser> GetUsers()
        {
            List<ApplicationUser> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Users.ToList();
            }
            return result;
        }

        public ICollection<ApplicationUser> GetUsers(string search)
        {
            List<ApplicationUser> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var query = db.Users.AsQueryable();
                if (!String.IsNullOrEmpty(search))
                {
                    query = query.Where(x => x.UserName.Contains(search) || x.Email.Contains(search) || ((x.first_name.ToString()) + (" ") + (x.last_name.ToString())).Contains(search));
                }
                result = query.ToList();
            }
            return result;
        }

        #endregion

        #region Certificate
        public ICollection<Certificate> GetCertificates()
        {
            ICollection<Certificate> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Certificates.ToList();
            }
            return result;
        }

		public ICollection<TodoTask> GetTodoTasks()
		{
			ICollection<TodoTask> result;
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				result = db.TodoTasks.Include("Courses")
									 .ToList();
			}
			return result;
		}
        public Certificate GetCertificate(int Id)
        {
            Certificate result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = db.Certificates.Find(Id);
            }
            return result;
        }

        public void AddCertificate(Certificate certificate)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Certificates.Add(certificate);
                db.SaveChanges();
            }
        }

        public void UpdateCertificate(Certificate certificate)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Certificates.Attach(certificate);
                db.Entry(certificate).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteCertificate(int Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Certificate certificate = db.Certificates.Find(Id);
                db.Certificates.Remove(certificate);
                db.SaveChanges();
            }
        }
        #endregion


        
		public void AddTodoTask(TodoTask todoTask, List<int> courseIds)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				db.TodoTasks.Add(todoTask);
				db.SaveChanges();
				foreach (int id in courseIds)
				{
					Course course = db.Courses.Find(id);
					if (course != null)
					{
						todoTask.Courses.Add(course);
					}
				}
				db.SaveChanges();
			}
		}

        
		public TodoTask GetTodoTask(int id)
		{
			TodoTask result;
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				result = db.TodoTasks.Find(id);
			}
			return result;
		}

		public TodoTask GetTodoTaskFull(int id)
		{
			TodoTask result;
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				result = db.TodoTasks.Include("Courses")
									 .Where(x => x.Id == id)
									 .FirstOrDefault();
			}
			return result;
		}

        
		public void UpdateTodoTask(TodoTask todoTask, List <int> courseIds)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				db.TodoTasks.Attach(todoTask);
				db.Entry(todoTask).Collection("Courses").Load();
				todoTask.Courses.Clear();
				db.SaveChanges();
				foreach (int id in courseIds)
				{
					Course course = db.Courses.Find(id);
					if (course != null)
					{
						todoTask.Courses.Add(course);
					}
				}
				db.Entry(todoTask).State = EntityState.Modified;
				db.SaveChanges();
			}
		}

		public void DeleteTodoTask(int id)
		{
			using (ApplicationDbContext db = new ApplicationDbContext())
			{
				TodoTask todoTask = db.TodoTasks.Find(id);
				db.TodoTasks.Remove(todoTask);
				db.SaveChanges();
			}
		}

        #region Friends

        public void AddFriend(Friend friend)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Friends.Add(friend);
                db.SaveChanges();
            }
        }

        public ICollection<ApplicationUser> GetNotifications(string userId)
        {
            ICollection<ApplicationUser> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var query = db.Users.Join(db.Friends,
                                        user => user.Id,
                                        friend => friend.senderId,
                                        (user, sender) => new { User = user, Sender = sender })
                                    .Where((x => x.User.Id == x.Sender.senderId && x.Sender.receiverId == userId && x.Sender.status == FriendStatus.Pending))
                                    .Select(x => x.User)
                                    .ToList();
                result = query;
            }
            return result;
        }

        public void AcceptRequest(string senderId, string receiverId)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var friends = db.Friends.Where(x => x.senderId == senderId && x.receiverId == receiverId).FirstOrDefault();
                db.Friends.Attach(friends);
                friends.status = FriendStatus.Approved;
                db.Entry(friends).State = EntityState.Modified;
                AddPersonalChat(friends.Id);
                db.SaveChanges();
            }
        }

        public ICollection<ApplicationUser> GetFriendList(string userId)
        {
            ICollection<ApplicationUser> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var query = db.Users.Join(db.Friends,
                                        user => user.Id,
                                        friend => friend.senderId == userId ? friend.receiverId : friend.senderId,
                                        (user, friendship) => new { User = user, Friendship = friendship })
                                    .Where(x => (userId == x.Friendship.senderId || userId  == x.Friendship.receiverId) && x.Friendship.status == FriendStatus.Approved)
                                    .Select(x => x.User)
                                    .ToList();
                result = query;
            }
            return result;
        }

        public Friend GetFriendship(string userId, string friendId)
        {
            Friend result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var friendship = db.Friends.Where(x => (userId == x.senderId && friendId == x.receiverId) ||
                                                       (friendId == x.senderId && userId == x.receiverId) &&
                                                        x.status == FriendStatus.Approved).FirstOrDefault();
                result = friendship;
            }
            return result;
        }

        #endregion

        #region Personal Chat

        public void AddPersonalChat(int friendshipId)
        {
            Chat chat = new Chat()
            {
                Id = friendshipId
            };
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Chats.Add(chat);
                db.SaveChanges();
            }
        }

        public Chat GetPersonalChat(int friendId)
        {
            Chat result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var chat = db.Chats.Find(friendId);
                result = chat;
            }
            return result;
        }

        #endregion

        #region Messages

        public ICollection<Message> GetMessages(int chatId)
        {
            ICollection<Message> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var messages = db.Messages.Where(x => x.ChatId == chatId).ToList();
                result = messages;
            }
            return result;
        }

        public void AddMessage(Message message)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Messages.Add(message);
                db.SaveChanges();
            }
        }

        #endregion

        #region Global Chat

        public ICollection<GlobalChatViewModel> GetGlobalChatHistory()
        {
            ICollection<GlobalChatViewModel> result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var GlobalChatHistory = db.GlobalChats.Join(db.Users,
                                                            sender => sender.UserId,
                                                            user => user.Id,
                                                            (sender, user) => new { Sender = sender, User = user })
                                                      .Where(x => x.User.Id == x.Sender.UserId)
                                                      .Select(x => new GlobalChatViewModel
                                                      {
                                                          UserName = x.User.UserName,
                                                          Sent = x.Sender.sent,
                                                          Message = x.Sender.message
                                                      })
                                                      .ToList();
                result = GlobalChatHistory;
            }
            return result;
        }

        #endregion

    }
}