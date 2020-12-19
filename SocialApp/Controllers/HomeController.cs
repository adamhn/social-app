using Microsoft.AspNet.Identity;
using SocialApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialApp.ViewModels;

namespace SocialApp.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext DbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public HomeController()
        {
            this.DbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
        }

        public async Task<ActionResult> Index()
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            var viewModel = new HomeViewModel
            {
                Users = DbContext.Users.ToList()
            };

            if (currentUser == null) return View(viewModel);

            ViewBag.Message = "Hey " + currentUser.Firstname + " " + currentUser.Lastname;
            viewModel.Users = DbContext.Users.Where(u => u.Id != currentUser.Id).ToList();

            return View(viewModel);
        }

        //TODO: home/user/id

        //[Route("home/released/{year}/{month:regex(\\d{4}):range(1, 12)}")]
        //public ActionResult ByReleaseYear(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}
    }
}