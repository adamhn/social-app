using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using SocialApp.Models;

namespace SocialApp.ViewModels
{
    public class SetInformationViewModel
    {
        [Required]
        [Display(Name = "Firstname")]
        [DataType(DataType.Text)]
        [StringLength(55)]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        [DataType(DataType.Text)]
        [StringLength(55, ErrorMessage = "{0} cannot be more then 100 characters long")]
        public string Lastname { get; set; }

        [Display(Name = "Work Company")]
        [StringLength(55)]
        public string Work { get; set; }

        [Display(Name = "Studying at")]
        [StringLength(55)]
        public string Study { get; set; }

        [Required]
        [MinimumAge(18)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birthdate")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public IEnumerable<RelationshipStatus> RelationshipStatus { get; set; }

        [Required]
        [Display(Name = "Relationship Status")]
        public byte RelationshipStatusId { get; set; }

        [Display(Name = "Hide from search?")]
        public bool IsHiddenFromSearch { get; set; }
    }

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