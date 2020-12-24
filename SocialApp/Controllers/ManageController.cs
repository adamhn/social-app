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


        public async Task<ActionResult> SetInformation()
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var relationshipStatus = DbContext.RelationshipStatus.ToList();

            var viewModel = new SetInformationViewModel
            {
                Firstname = currentUser.Firstname,
                Lastname = currentUser.Lastname,
                Work = currentUser.Work,
                Study = currentUser.Study,
                BirthDate = currentUser.BirthDate,
                RelationshipStatus = relationshipStatus
            };
            return View(viewModel);
        }

        // POST: /Manage/SetInformation
        [HttpPost]
        public async Task<ActionResult> SetInformation(SetInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            currentUser.Firstname = model.Firstname;
            currentUser.Lastname = model.Lastname;
            currentUser.Work = model.Work;
            currentUser.Study = model.Study;
            currentUser.BirthDate = model.BirthDate;
            currentUser.RelationshipStatusId = model.RelationshipStatusId;

            await UserManager.UpdateAsync(currentUser);

            return RedirectToAction("Details", "Users", new { userId = currentUser.Id });
        }

        // GET: /Manage/SetPhoto
        [HttpGet]
        public ActionResult SetPhoto()
        {
            return View();
        }

        // POST: /Manage/SetPhoto
        [HttpPost]
        public async Task<ActionResult> SetPhoto(HttpPostedFileBase profilePicture)
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

            // convert image stream to byte array
            var image = new byte[profilePicture.ContentLength];
            await profilePicture.InputStream.ReadAsync(image, 0, Convert.ToInt32(profilePicture.ContentLength));

            currentUser.Picture = image;

            await UserManager.UpdateAsync(currentUser);

            return RedirectToAction("Details", "Users", new { userId = currentUser.Id });
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