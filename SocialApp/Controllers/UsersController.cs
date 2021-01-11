using Microsoft.AspNet.Identity;
using SocialApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialApp.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using SocialApp.Helpers;

namespace SocialApp.Controllers
{
    public class UsersController : Controller
    {
        protected ApplicationDbContext DbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        protected Utils Utils { get; set; }

        public UsersController()
        {
            this.DbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
            this.Utils = new Utils();
        }

        protected override void Dispose(bool disposing)
        {
            DbContext.Dispose();
        }

        // GET Users
        [AllowAnonymous]
        public async Task<ActionResult> Index(string searchText)
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            var viewModel = new ViewModels.UsersViewModel
            {
                Users = DbContext.Users
                    .Where(u => u.Firstname.Contains(searchText) || searchText == null)
                    .Where(u => u.IsHiddenFromSearch == false)
                    .OrderBy(u => u.Firstname)
                    .ToList(),
                CurrentUser = currentUser
            };

            if (currentUser == null) return View(viewModel);

            viewModel.Users = DbContext.Users
                .Where(x => (x.Firstname.Contains(searchText) || searchText == null) && x.Id != currentUser.Id)
                .Where(x => x.IsHiddenFromSearch == false)
                .OrderBy(u => u.Firstname)
                .ToList();

            return View(viewModel); 
        }

        
        // GET Users/Details/{userId}
        public async Task<ActionResult> Details(string userId)
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var detailsUser = DbContext.Users.Include(u => u.RelationshipStatus).SingleOrDefault(u => u.Id == userId);
            var friendRequest = Utils.FindFriendRequestMatch(userId, currentUser.Id);
            var friends = Utils.FindFriendsFor(userId);

            if (detailsUser == null || friendRequest == null) return HttpNotFound();

            var viewModel = new UserDetailsViewModel
            {
                User = detailsUser,
                FriendRequestFlag = friendRequest.FriendRequestFlag,
                Friends = friends
            };

            return View(viewModel);
        }

        // GET Users/SendFriendRequest/{friendId}
        public async Task<ActionResult> SendFriendRequest(string friendId)
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var friendUser = DbContext.Users.SingleOrDefault(u => u.Id == friendId);

            if (friendUser == null) return RedirectToAction("Details", new { userId = friendId });

            var newFriend = new Friend
            {
                RequestedById = currentUser.Id,
                RequestedToId = friendUser.Id,
                FriendRequestFlag = FriendRequestFlag.Awaiting,
                RequestTime = DateTime.Now
            };

            DbContext.Friends.Add(newFriend);
            await DbContext.SaveChangesAsync();

            return RedirectToAction("Details", new { userId = friendId });
        }

        [AllowAnonymous]
        public ActionResult GetUserPhoto(string userId)
        {
            var user = DbContext.Users.Find(userId);
            if (user == null) return HttpNotFound();
            return new FileContentResult(user.Picture, "image/jpeg");
        }
    }
}