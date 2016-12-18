using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NETboard.Models
{
    public class StudentNotViewed
    {
        public int Id { get; set; }
        public virtual ApplicationUser SpecificStudent { get; set; }
        public virtual Announcement SpecificAnnouncement { get; set; }
        public virtual int SpecificStudentId { get; set; }
        public virtual int SpecificAnnouncementId { get; set; }
    }
}