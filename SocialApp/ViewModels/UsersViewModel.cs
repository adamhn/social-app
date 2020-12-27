using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SocialApp.Models;

namespace SocialApp.ViewModels
{
    public class UsersViewModel
    {
        [Required]
        public List<ApplicationUser> Users { get; set; }
        public ApplicationUser CurrentUser { get; set; }

    }

    public class UserDetailsViewModel
    {
        [Required] public ApplicationUser User { get; set; }

        [Required] public FriendRequestFlag FriendRequestFlag { get; set; }

        [Required] public List<ApplicationUser> Friends { get; set; }

        [StringLength(155, MinimumLength = 20, ErrorMessage = "Post cannot be longer then 155 characters and less than 20 characters long")]
        public Post Post { get; set; }

        public string GetFullName()
        {
            return User.Firstname + " " + User.Lastname;
        }
    }


}