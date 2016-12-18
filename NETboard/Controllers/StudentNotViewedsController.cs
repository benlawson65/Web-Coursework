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
    public class StudentNotViewedsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentNotVieweds
        public ActionResult Index()
        {
            var studentNotVieweds = db.StudentNotVieweds.Include(s => s.SpecificAnnouncement);
            return View(studentNotVieweds.ToList());
        }

        // GET: StudentNotVieweds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentNotViewed studentNotViewed = db.StudentNotVieweds.Find(id);
            if (studentNotViewed == null)
            {
                return HttpNotFound();
            }
            return View(studentNotViewed);
        }

        // GET: StudentNotVieweds/Create
        public ActionResult Create()
        {
            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "announcementTitle");
            return View();
        }

        // POST: StudentNotVieweds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SpecificStudentId,SpecificAnnouncementId")] StudentNotViewed studentNotViewed)
        {
            if (ModelState.IsValid)
            {
                db.StudentNotVieweds.Add(studentNotViewed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "announcementTitle", studentNotViewed.SpecificAnnouncementId);
            return View(studentNotViewed);
        }

        // GET: StudentNotVieweds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentNotViewed studentNotViewed = db.StudentNotVieweds.Find(id);
            if (studentNotViewed == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "announcementTitle", studentNotViewed.SpecificAnnouncementId);
            return View(studentNotViewed);
        }

        // POST: StudentNotVieweds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SpecificStudentId,SpecificAnnouncementId")] StudentNotViewed studentNotViewed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentNotViewed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "announcementTitle", studentNotViewed.SpecificAnnouncementId);
            return View(studentNotViewed);
        }

        // GET: StudentNotVieweds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentNotViewed studentNotViewed = db.StudentNotVieweds.Find(id);
            if (studentNotViewed == null)
            {
                return HttpNotFound();
            }
            return View(studentNotViewed);
        }

        // POST: StudentNotVieweds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentNotViewed studentNotViewed = db.StudentNotVieweds.Find(id);
            db.StudentNotVieweds.Remove(studentNotViewed);
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
