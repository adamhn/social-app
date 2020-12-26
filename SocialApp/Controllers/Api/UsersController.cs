using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialApp.Models;

namespace SocialApp.Controllers.Api
{
    public class UsersController : ApiController
    {
        private ApplicationDbContext DbContext;
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public UsersController()
        {
            DbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));

        }

        // GET /api/friends
        public IEnumerable<Friend> GetFriends()
        {
            var loggedInUser = UserManager.FindById(User.Identity.GetUserId());
            //var users = DbContext.Users.ToList();
            var friends = DbContext.Friends.Where(f => 
                    ((f.RequestedToId == loggedInUser.Id) || (f.RequestedById == loggedInUser.Id)) 
                    && f.FriendRequestFlag == FriendRequestFlag.Approved).ToList();

            return friends;
        }

        // GET /api/user/1
        public ApplicationUser GetUser(string id)
        {
            var user = DbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return user;
        }


    }
}
