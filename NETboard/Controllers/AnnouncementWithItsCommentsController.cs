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
    public class AnnouncementWithItsCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AnnouncementWithItsComments
        public ActionResult Index()
        {
            return View(db.AnnouncementWithItsComments);
        }

        // GET: AnnouncementWithItsComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnouncementWithItsComments announcementWithItsComments = db.AnnouncementWithItsComments;
            if (announcementWithItsComments == null)
            {
                return HttpNotFound();
            }
            return View(announcementWithItsComments);
        }

        // GET: AnnouncementWithItsComments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnnouncementWithItsComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] AnnouncementWithItsComments announcementWithItsComments)
        {
            if (ModelState.IsValid)
            {
                db.AnnouncementWithItsComments=(announcementWithItsComments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(announcementWithItsComments);
        }

        // GET: AnnouncementWithItsComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnouncementWithItsComments announcementWithItsComments = db.AnnouncementWithItsComments;
            if (announcementWithItsComments == null)
            {
                return HttpNotFound();
            }
            return View(announcementWithItsComments);
        }

        // POST: AnnouncementWithItsComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] AnnouncementWithItsComments announcementWithItsComments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(announcementWithItsComments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(announcementWithItsComments);
        }

        // GET: AnnouncementWithItsComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnouncementWithItsComments announcementWithItsComments = db.AnnouncementWithItsComments;
            if (announcementWithItsComments == null)
            {
                return HttpNotFound();
            }
            return View(announcementWithItsComments);
        }

        // POST: AnnouncementWithItsComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnnouncementWithItsComments announcementWithItsComments = db.AnnouncementWithItsComments;
            db.AnnouncementWithItsComments=null;
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
