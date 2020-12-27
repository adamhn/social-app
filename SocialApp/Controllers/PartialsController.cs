using SocialApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialApp.Models;

namespace SocialApp.Controllers
{
    [ChildActionOnly]
    public class PartialsController : Controller
    {
        protected ApplicationDbContext DbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public PartialsController()
        {
            this.DbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
        }

        // GET: Navigation
        [AllowAnonymous]
        public ActionResult Navigation()
        {
            var loggedInUser = UserManager.FindById(User.Identity.GetUserId());

            if (loggedInUser == null)
                return PartialView("_Navigation");

            var users = DbContext.Users.ToList();
            var currentUser = users.SingleOrDefault(u => u.Id == loggedInUser.Id);
            var friends = DbContext.Friends.Where(f => f.RequestedToId == loggedInUser.Id).ToList();
            var friendRequests = friends
                .Where(friend => friend.FriendRequestFlag == FriendRequestFlag.Awaiting)
                .Select(friend => users.SingleOrDefault(u => u.Id == friend.RequestedById))
                .ToList();

            var viewModel = new NavigationViewModel
            {
                Friends = friends,
                FriendRequests = friendRequests,
                CurrentUser = currentUser
            };

            return PartialView("_Navigation", viewModel);
        }
    }
}