using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NETboard.Models;
using Microsoft.AspNet.Identity;

namespace NETboard.Controllers
{
    public class AnnouncementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Announcements
        public ActionResult Index()
        {
            return View(); //View(db.Announcements.ToList());
        }
        public void InsertsAnnouncements()
        {
            AnnouncementWithItsComments link = new AnnouncementWithItsComments();
            ICollection<Announcement> ann = db.Announcements.ToList();
            link.AnnouncementsList = new List<Announcement>();
            foreach (var element in ann)
            {
                link.AnnouncementsList.Add(element);
            }
            db.AnnouncementWithItsComments = link;
            db.SaveChanges();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxInsertComment(int? id, AnnouncementWithItsComments commentInstance)
        {
            //finds announcement that comment should be placed on
            Announcement announcement = db.Announcements.Find(id);

            Comment comment = new Comment();
            comment.commentContent = commentInstance.CommentModel.commentContent;
            comment.userName = "something";
            comment.timeStamp = DateTime.Now.ToString("h:mm:ss tt");
            announcement.listOfComments.Add(comment);
            InsertsAnnouncements();
            db.SaveChanges();
            //ModelState.Clear();

            return PartialView("_Announcement",db.AnnouncementWithItsComments);
        }
        public ActionResult BuildAnnouncements()
        {
            InsertsAnnouncements();
            return PartialView("_Announcement",db.AnnouncementWithItsComments);
        }

        // GET: Announcements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // GET: Announcements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Announcements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,announcementTitle,announcementContent,annoucmementTimeStamp,staffName,staffID")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Announcements.Add(announcement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(announcement);
        }

        //used to update the announcements feed when new announcement added from same page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxCreate([Bind(Include = "Id,announcementTitle,announcementContent")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.announcementTimeStamp = DateTime.Now.ToString("h:mm:ss tt");
                string currentUserID = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserID);
                announcement.staffName = currentUser;
                announcement.listOfComments = new List<Comment>();
                db.Announcements.Add(announcement);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            //returning the set of announcements (which is a partial view) with added anouncement
            return BuildAnnouncements();
            //.Where(x => x.staffName == currentUser)
        }

        // GET: Announcements/Edit/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,announcementTitle,announcementContent,annoucmementTimeStamp,staffName,staffID")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Announcements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Announcement announcement = db.Announcements.Find(id);
            db.Announcements.Remove(announcement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxDeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            db.Comments.Remove(comment);
            db.SaveChanges();
            InsertsAnnouncements();

            db.SaveChanges();
            return PartialView("_Announcement", db.AnnouncementWithItsComments); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxDeleteAnnouncement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            //ICollection<Comment> allComments = db.Comments;
            
            //db.SaveChanges();

            //AnnouncementWithItsComments link = new AnnouncementWithItsComments();
            ICollection<Comment> AllComments = db.Comments.ToList();
            //link.AnnouncementsList = new List<Announcement>();

            List<Comment> CommentsList = new List<Comment>();
            foreach (var element in announcement.listOfComments)
            {
               //db.Comments.Remove(db.Comments.Find(element.Id));
                CommentsList.Add(element);
            }

            
            foreach (var element1 in CommentsList) {
                db.Comments.Remove(element1);
            }
            
            db.SaveChanges();
            db.Announcements.Remove(announcement);
            
            db.SaveChanges();
            InsertsAnnouncements();
            return PartialView("_Announcement", db.AnnouncementWithItsComments);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
