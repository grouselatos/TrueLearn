﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using TrueLearn.Models;


[assembly: OwinStartupAttribute(typeof(TrueLearn.Startup))]
namespace TrueLearn
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
			createRolesandUsers();
            app.MapSignalR();
		}

		private void createRolesandUsers()
		{
			ApplicationDbContext db = new ApplicationDbContext();

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

			if (!roleManager.RoleExists("Admin"))
			{
				var role = new IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);

				var user = new ApplicationUser();
				user.UserName = "admin";
				user.Email = "admin@gmail.com";
				user.birth_date = new DateTime(2000, 1, 1);
				user.country = "Greece";
				user.first_name = "admin";
				user.last_name = "admin";
				string userPWD = "iamtheadmin";
				var chkUser = UserManager.Create(user, userPWD);

				if (chkUser.Succeeded)
				{
					var result1 = UserManager.AddToRole(user.Id, "Admin");
				}
			}

			if (!roleManager.RoleExists("PremiumUser"))
			{
				var role = new IdentityRole();
				role.Name = "PremiumUser";
				roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "premium";
                user.Email = "premium@gmail.com";
                user.birth_date = new DateTime(1986, 4, 8);
                user.country = "Greece";
                user.first_name = "premium";
                user.last_name = "premium";
                string userPWD = "iamthepremium";
                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "PremiumUser");
                }
            }

			if (!roleManager.RoleExists("FreeUser"))
			{
				var role = new IdentityRole();
				role.Name = "FreeUser";
				roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "free";
                user.Email = "freeuser@gmail.com";
                user.birth_date = new DateTime(1985, 5, 6);
                user.country = "Greece";
                user.first_name = "free";
                user.last_name = "free";
                string userPWD = "iamthefree";
                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "FreeUser");
                }
			}
		}
	}
}