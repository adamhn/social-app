using Microsoft.AspNet.Identity;
using SocialApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SocialApp.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public HomeController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            ApplicationUser currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (currentUser != null)
            {
                ViewBag.Message = "Hey " + currentUser.Firstname  + " " + currentUser.Lastname;
                return View();
            } 

            ViewBag.Message = "Your application description page for /about. Hey anonymos";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your application description page for /contact.";
            return View();
        }

        [Route("home/released/{year}/{month:regex(\\d{4}):range(1, 12)}")]
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}