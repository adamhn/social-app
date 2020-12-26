using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialApp.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string RequestedById { get; set; }
        public string RequestedToId { get; set; }
        public DateTime? RequestTime { get; set; }
        public FriendRequestFlag FriendRequestFlag { get; set; }
    }
}