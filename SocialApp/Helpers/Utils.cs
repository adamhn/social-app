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

        /// <summary>
        /// Searches in all matching friend request for the two parties
        /// This to make sure we find a unique friend request no matter
        /// who sent the friend request of the two parties.
        /// </summary>
        /// <param name="userIdOne">User one</param>
        /// <param name="userIdTwo">User two</param>
        /// <returns>A single friend request</returns>
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

        /// <summary>
        /// Get friends for user id
        /// </summary>
        /// <param name="userId">User Id to search friends for</param>
        /// <returns>List of approved friends for param user id</returns>
        public List<ApplicationUser> FindFriendsFor(string userId)
        {
            var approvedRequests = DbContext.Friends
                .Where(f => 
                            (f.RequestedById == userId || f.RequestedToId == userId) 
                            && f.FriendRequestFlag == FriendRequestFlag.Approved).ToList();
            var friends = new List<ApplicationUser>();

            foreach (var approvedRequest in approvedRequests)
            {
                if (approvedRequest.RequestedById == userId)
                    friends.Add(DbContext.Users.SingleOrDefault(u => u.Id == approvedRequest.RequestedToId));

                if (approvedRequest.RequestedToId == userId)
                    friends.Add(DbContext.Users.SingleOrDefault(u => u.Id == approvedRequest.RequestedById));
            }
            
            return friends;
        }
    }
}