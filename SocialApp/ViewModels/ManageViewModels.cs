using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace SocialApp.Models
{
    public class IndexViewModel { }

    public class SetInformationViewModel
    {
        public ApplicationUser CurrentUser { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        [StringLength(100)]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "{0} cannot be more then 100 characters long")]
        public string Lastname { get; set; }
    }

    public class SetPhotoViewModel { }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}