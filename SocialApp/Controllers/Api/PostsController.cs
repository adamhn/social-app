using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialApp.Helpers;
using SocialApp.Models;

namespace SocialApp.Controllers.Api
{
    public class PostsController : ApiController
    {
        protected ApplicationDbContext DbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }
        protected Utils Utils { get; set; }

        public PostsController()
        {
            this.DbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
            this.Utils = new Utils();
        }

        // GET /api/posts
        [HttpGet]
        [Route("api/posts/GetPosts/{id}")]
        public List<Post> GetPosts(string id)
        {
            return Utils.GetAllPosts(id);
        }

        // POST /api/posts
        [HttpPost]
        [Route("api/Posts/CreatePost")]
        public HttpResponseMessage CreatePost(Post post)
        {
            var postedByUser = DbContext.Users.SingleOrDefault(u => u.Id == post.PostedById);
            post.PostedDatetime = DateTime.Now;

            if (!ModelState.IsValid || postedByUser == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            post.PostedByFullname = postedByUser.GetFullName();

            DbContext.Posts.Add(new Post
            {
                Text = post.Text,
                PostedById = post.PostedById,
                PostedToId = post.PostedToId,
                PostedDatetime = post.PostedDatetime,
                PostedByFullname = post.PostedByFullname
            });

            DbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Successfully added new post");
        }

    }
}
