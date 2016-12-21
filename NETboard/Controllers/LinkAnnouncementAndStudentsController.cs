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
    public class LinkAnnouncementAndStudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LinkAnnouncementAndStudents
        public ActionResult Index()
        {
            var linkAnnouncementAndStudents = db.LinkAnnouncementAndStudents.Include(l => l.SpecificAnnouncement);
            return View(linkAnnouncementAndStudents.ToList());
        }

        // GET: LinkAnnouncementAndStudents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkAnnouncementAndStudent linkAnnouncementAndStudent = db.LinkAnnouncementAndStudents.Find(id);
            if (linkAnnouncementAndStudent == null)
            {
                return HttpNotFound();
            }
            return View(linkAnnouncementAndStudent);
        }

        // GET: LinkAnnouncementAndStudents/Create
        public ActionResult Create()
        {
            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "Title");
            return View();
        }

        // POST: LinkAnnouncementAndStudents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SpecificStudentId,SpecificAnnouncementId")] LinkAnnouncementAndStudent linkAnnouncementAndStudent)
        {
            if (ModelState.IsValid)
            {
                db.LinkAnnouncementAndStudents.Add(linkAnnouncementAndStudent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "Title", linkAnnouncementAndStudent.SpecificAnnouncementId);
            return View(linkAnnouncementAndStudent);
        }

        // GET: LinkAnnouncementAndStudents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkAnnouncementAndStudent linkAnnouncementAndStudent = db.LinkAnnouncementAndStudents.Find(id);
            if (linkAnnouncementAndStudent == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "Title", linkAnnouncementAndStudent.SpecificAnnouncementId);
            return View(linkAnnouncementAndStudent);
        }

        // POST: LinkAnnouncementAndStudents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SpecificStudentId,SpecificAnnouncementId")] LinkAnnouncementAndStudent linkAnnouncementAndStudent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(linkAnnouncementAndStudent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecificAnnouncementId = new SelectList(db.Announcements, "Id", "Title", linkAnnouncementAndStudent.SpecificAnnouncementId);
            return View(linkAnnouncementAndStudent);
        }

        // GET: LinkAnnouncementAndStudents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LinkAnnouncementAndStudent linkAnnouncementAndStudent = db.LinkAnnouncementAndStudents.Find(id);
            if (linkAnnouncementAndStudent == null)
            {
                return HttpNotFound();
            }
            return View(linkAnnouncementAndStudent);
        }

        // POST: LinkAnnouncementAndStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LinkAnnouncementAndStudent linkAnnouncementAndStudent = db.LinkAnnouncementAndStudents.Find(id);
            db.LinkAnnouncementAndStudents.Remove(linkAnnouncementAndStudent);
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
