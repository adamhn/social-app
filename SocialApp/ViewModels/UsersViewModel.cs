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

        public string GetFullName()
        {
            return User.Firstname + " " + User.Lastname;
        }
    }


}