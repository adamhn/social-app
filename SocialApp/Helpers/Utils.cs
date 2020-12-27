using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialApp.Models;

namespace SocialApp.Helpers
{
    public class Utils
    {
        protected ApplicationDbContext DbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public Utils()
        {
            this.DbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
        }
        public Friend FindFriendRequestMatch(string userIdOne, string userIdTwo)
        {
            // Get all matching requests
            var friendRequests = DbContext.Friends.Where(f => f.RequestedById == userIdOne 
                                                              || f.RequestedToId == userIdOne 
                                                              || f.RequestedById == userIdTwo 
                                                              || f.RequestedToId == userIdTwo);
            var singleRequest = new Friend();

            // Find a matching request no matter who sent the request of the two parties
            foreach (var friendRequest in friendRequests)
            {
                if (friendRequest.RequestedById == userIdOne && friendRequest.RequestedToId == userIdTwo)
                    singleRequest = friendRequest;

                if (friendRequest.RequestedById == userIdTwo && friendRequest.RequestedToId == userIdOne)
                    singleRequest = friendRequest;
            }

            return singleRequest;
        }
    }
}