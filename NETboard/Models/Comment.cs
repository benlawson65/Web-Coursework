using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NETboard.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Display(Name = "Comment:")]
        public string CommentContent { get; set; }
        public string TimeStamp { get; set; }
        public string UserName { get; set; }

    }
}