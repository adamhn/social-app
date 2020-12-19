using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialApp.Models;

namespace SocialApp.ViewModels
{
    public class HomeViewModel
    {
        public List<ApplicationUser> Users { get; set; }
    }
}