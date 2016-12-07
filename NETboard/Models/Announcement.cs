using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NETboard.Models
{
    public class Announcement
    {

        public int Id { get; set; }
        public string announcementTitle { get; set; }
        public string announcementContent { get; set; }
        public string annoucmementTimeStamp { get; set; }

        public string staffName { get; set; }
        public int staffID { get; set; }



    }
}