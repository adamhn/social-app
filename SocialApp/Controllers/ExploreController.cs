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
using Microsoft.AspNet.Identity.Owin;

namespace SocialApp.Controllers
{
    public class ExploreController : Controller
    {
        protected ApplicationDbContext DbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public ExploreController()
        {
            this.DbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.DbContext));
        }

        public async Task<ActionResult> Index()
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            var viewModel = new ExploreViewModel
            {
                Users = DbContext.Users.ToList()
            };

            if (currentUser == null) return View(viewModel);

            ViewBag.Message = "Hey " + currentUser.Firstname + " " + currentUser.Lastname;
            viewModel.Users = DbContext.Users.Where(u => u.Id != currentUser.Id).ToList();

            return View(viewModel);
        }

        public ActionResult Details(string id)
        {
            var user = DbContext.Users.Find(id);

            if (user == null) return HttpNotFound();

            return View(user);
        }

        public FileContentResult Photo(string userId)
        {
            // get EF Database (maybe different way in your applicaiton)
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            // find the user. I am skipping validations and other checks.
            var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();

            // check if there is no image and in that case return default image
            // C:\Users\adam\Source\Repos\social-app\SocialApp\
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            byte[] defaultImageBytes = System.IO.File.ReadAllBytes(startupPath + "Content\\Images\\default-image.jpg");

            //string imageString64Date = Convert.ToBase64String(defaultImageBytes);
            if (user.Picture == null) return new FileContentResult(defaultImageBytes, "image/jpeg");

            return new FileContentResult(user.Picture, "image/jpeg");
        }

        //TODO: home/user/id

        //[Route("home/released/{year}/{month:regex(\\d{4}):range(1, 12)}")]
        //public ActionResult ByReleaseYear(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}
    }
}