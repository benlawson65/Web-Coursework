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
    public class StudentViewedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentVieweds
        public ActionResult Index()
        {
            var studentVieweds = db.StudentVieweds.Include(s => s.SpecificAnnouncement);
            return View(studentVieweds.ToList());
        }

        // GET: StudentVieweds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentViewed studentViewed = db.StudentVieweds.Find(id);
            if (studentViewed == null)
            {
                return HttpNotFound();
            }
            return View(studentViewed);
        }

        // GET: StudentVieweds/Create
        public ActionResult Create()
        {
            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "announcementTitle");
            return View();
        }

        // POST: StudentVieweds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SpecificStudentId,SpecificAnnouncementId")] StudentViewed studentViewed)
        {
            if (ModelState.IsValid)
            {
                db.StudentVieweds.Add(studentViewed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "announcementTitle", studentViewed.SpecificAnnouncementId);
            return View(studentViewed);
        }

        // GET: StudentVieweds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentViewed studentViewed = db.StudentVieweds.Find(id);
            if (studentViewed == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "announcementTitle", studentViewed.SpecificAnnouncementId);
            return View(studentViewed);
        }

        // POST: StudentVieweds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SpecificStudentId,SpecificAnnouncementId")] StudentViewed studentViewed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentViewed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "announcementTitle", studentViewed.SpecificAnnouncementId);
            return View(studentViewed);
        }

        // GET: StudentVieweds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentViewed studentViewed = db.StudentVieweds.Find(id);
            if (studentViewed == null)
            {
                return HttpNotFound();
            }
            return View(studentViewed);
        }

        // POST: StudentVieweds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentViewed studentViewed = db.StudentVieweds.Find(id);
            db.StudentVieweds.Remove(studentViewed);
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
