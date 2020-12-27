using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialApp.Dtos;
using SocialApp.Models;

namespace SocialApp.Controllers.Api
{
    public class UsersController : ApiController
    {
        protected ApplicationDbContext DbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public UsersController()
        {
            this.DbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));

        }

        //// GET /api/friends
        //public IEnumerable<Friend> GetFriends()
        //{
        //    var loggedInUser = UserManager.FindById(User.Identity.GetUserId());
        //    //var users = DbContext.Users.ToList();
        //    var friends = DbContext.Friends.Where(f => 
        //            ((f.RequestedToId == loggedInUser.Id) || (f.RequestedById == loggedInUser.Id)) 
        //            && f.FriendRequestFlag == FriendRequestFlag.Approved).ToList();

        //    return DbContext.Friends.ToList();
        //}

        // GET /api/users
        public IEnumerable<UserDto> GetUsers()
        {
            return DbContext.Users.ToList().Select(Mapper.Map<ApplicationUser, UserDto>);
        }

        // GET /api/user/1
        public UserDto GetUser(string id)
        {
            var user = DbContext.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Mapper.Map<ApplicationUser, UserDto>(user);
        }


        // POST /api/posts
        //[HttpPost]
        //public Post CreatePost(Post post)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    // Add post to context and save changes dbContext.saveChanges()
        //    // return the newly created post
        //}


        // With mapper
        // POST /api/posts
        //[HttpPost]
        //public PostDto CreatePost(PostDto postDto)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);

        //    var post = Mapper.Map<PostDto, Post>(postDto);
        //    DbContext.Posts.Add(post);
        //    DbContext.SaveChanges();
        //    postDto.Id = post.Id;
        //    return postDto;
        //}
    }
}
