using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialApp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string PostedById { get; set; }
        public string PostedToId { get; set; }
        public string PostedByFullname { get; set; }
        public DateTime? PostedDatetime { get; set; }
    }
}