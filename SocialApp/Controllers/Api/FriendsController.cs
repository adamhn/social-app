using System;
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
    public class FriendsController : ApiController
    {
        protected ApplicationDbContext DbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public FriendsController()
        {
            this.DbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
        }

        // PUT /api/Friends/UpdateFriendRequest/{userId}/{flag}
        [HttpPut]
        [Route("api/Friends/UpdateFriendRequest/{userId}/{flag}")]
        public HttpResponseMessage UpdateFriendRequest(string userId, string flag)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(flag))
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var signedInUser = UserManager.FindById(User.Identity.GetUserId());

            var friendRequest = DbContext.Friends
                .SingleOrDefault(f => f.RequestedToId == signedInUser.Id && f.RequestedById == userId);

            if (friendRequest == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            friendRequest.FriendRequestFlag = (FriendRequestFlag) Enum.Parse(typeof(FriendRequestFlag), flag);
            DbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
    }
}
