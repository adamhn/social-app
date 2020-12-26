using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SocialApp.Models;

namespace SocialApp.Dtos
{
    public class UserDto
    {
        [RequiredAttribute]
        [StringLength(55)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(55)]
        public string Lastname { get; set; }

        [StringLength(55)]
        public string Work { get; set; }

        [StringLength(55)]
        public string Study { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public byte RelationshipStatusId { get; set; }
    }
}