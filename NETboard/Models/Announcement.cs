using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NETboard.Models
{
    public class Announcement
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        
        public string TimeStamp { get; set; }

        public string staffName { get; set; }
        public virtual ICollection<Comment> ListOfComments { get; set; }

        public List<string> WhoNotViewed { get; set; }

    }
}