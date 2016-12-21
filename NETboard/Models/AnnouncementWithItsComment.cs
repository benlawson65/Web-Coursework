using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NETboard.Models
{
    public class AnnouncementWithItsComment
    {
        public int Id { get; set; }
        public virtual ICollection<Announcement> AnnouncementsList { get; set; }
        public virtual Comment CommentModel { get; set; }
    }
}