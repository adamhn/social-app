using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialApp.Models;

namespace SocialApp.ViewModels
{
    public class NavigationViewModel
    {
        public List<Friend> Friends { get; set; }
        public List<ApplicationUser> FriendRequests { get; set; }
        public ApplicationUser CurrentUser { get; set; }
    }
}