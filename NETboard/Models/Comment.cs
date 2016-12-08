using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NETboard.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string commentContent { get; set; }
        public string timeStamp { get; set; }
        public string userName { get; set; }
        public int announcementID { get; set; }

    }
}