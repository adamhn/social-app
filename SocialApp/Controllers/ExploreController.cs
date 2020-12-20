﻿using Microsoft.AspNet.Identity;
using SocialApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialApp.ViewModels;
using Microsoft.AspNet.Identity.Owin;

namespace SocialApp.Controllers
{
    public class ExploreController : Controller
    {
        protected ApplicationDbContext DbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public ExploreController()
        {
            this.DbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
        }

        public async Task<ActionResult> Index()
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            var viewModel = new ExploreViewModel
            {
                Users = DbContext.Users.OrderBy(u => u.Firstname).ToList()
            };

            if (currentUser == null) return View(viewModel);

            viewModel.Users = DbContext.Users.Where(u => u.Id != currentUser.Id).OrderBy(u => u.Firstname).ToList();

            return View(viewModel);
        }

        public ActionResult Details(string id)
        {
            var user = DbContext.Users.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        public ActionResult GetUserPhoto(string userId)
        {
            var user = DbContext.Users.Find(userId);
            if (user == null) return HttpNotFound();
            return new FileContentResult(user.Picture, "image/jpeg");
        }
    }
}