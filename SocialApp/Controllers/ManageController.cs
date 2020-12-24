using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SocialApp.Models;
using SocialApp.ViewModels;

namespace SocialApp.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        protected ApplicationDbContext DbContext { get; set; }

        public ManageController()
        {
            this.DbContext = new ApplicationDbContext();
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: /Manage/
        public ActionResult Index()
        {
            return RedirectToAction("SetInformation", "Manage");
        }


        public ActionResult SetInformation()
        {
            var relationshipStatus = DbContext.RelationshipStatus.ToList();

            return View();
        }

        // POST: /Manage/SetInformation
        [HttpPost]
        public async Task<ActionResult> SetInformation(SetInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
        }

        // GET: /Manage/SetPhoto
        [HttpGet]
        public ActionResult SetPhoto()
        {
            return View();
        }

        // POST: /Manage/SetPhoto
        [HttpPost]
        public ActionResult SetPhoto(HttpPostedFileBase ProfilePicture)
        {
            // get EF Database (maybe different way in your applicaiton)
            var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

            // find the user. I am skipping validations and other checks.
            var userid = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userid).FirstOrDefault();

            // convert image stream to byte array
            byte[] image = new byte[ProfilePicture.ContentLength];
            ProfilePicture.InputStream.Read(image, 0, Convert.ToInt32(ProfilePicture.ContentLength));

            user.Picture = image;

            // save changes to database
            db.SaveChanges();

            return RedirectToAction("Index", "Users");
        }


        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Users");
            }
            AddErrors(result);
            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            { 
                _userManager.Dispose();
                _userManager = null;
            }

            DbContext.Dispose();

            base.Dispose(disposing);
        }

#region Helpers
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

#endregion
    }
}