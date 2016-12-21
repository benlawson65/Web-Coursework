using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NETboard.Models;

namespace NETboard.Controllers
{
    public class AnnouncementWithItsCommentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AnnouncementWithItsComment
        public ActionResult Index()
        {
            return View(db.AnnouncementWithItsComment);
        }

        // GET: AnnouncementWithItsComment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnouncementWithItsComment AnnouncementWithItsComment = db.AnnouncementWithItsComment;
            if (AnnouncementWithItsComment == null)
            {
                return HttpNotFound();
            }
            return View(AnnouncementWithItsComment);
        }

        // GET: AnnouncementWithItsComment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnnouncementWithItsComment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] AnnouncementWithItsComment AnnouncementWithItsComment)
        {
            if (ModelState.IsValid)
            {
                db.AnnouncementWithItsComment=(AnnouncementWithItsComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(AnnouncementWithItsComment);
        }

        // GET: AnnouncementWithItsComment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnouncementWithItsComment AnnouncementWithItsComment = db.AnnouncementWithItsComment;
            if (AnnouncementWithItsComment == null)
            {
                return HttpNotFound();
            }
            return View(AnnouncementWithItsComment);
        }

        // POST: AnnouncementWithItsComment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] AnnouncementWithItsComment AnnouncementWithItsComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(AnnouncementWithItsComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(AnnouncementWithItsComment);
        }

        // GET: AnnouncementWithItsComment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnouncementWithItsComment AnnouncementWithItsComment = db.AnnouncementWithItsComment;
            if (AnnouncementWithItsComment == null)
            {
                return HttpNotFound();
            }
            return View(AnnouncementWithItsComment);
        }

        // POST: AnnouncementWithItsComment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnnouncementWithItsComment AnnouncementWithItsComment = db.AnnouncementWithItsComment;
            db.AnnouncementWithItsComment=null;
            db.SaveChanges();
            return RedirectToAction("Index");
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
