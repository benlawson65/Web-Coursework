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

        public ActionResult Index(Announcement announcement)
        {

            string currentUserID = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserID);
            

            return View(announcement); //View(db.Announcements.ToList());
        }
        public void InsertsAnnouncements()
        {
            AnnouncementWithItsComments link = new AnnouncementWithItsComments();

            //find students that haven't viewed an announcement
            AllAnnouncementsToUsers();
            SeenAnnouncement();
            NotSeenAnnouncement();

            ICollection<Announcement> ann = db.Announcements.ToList();
            link.AnnouncementsList = new List<Announcement>();
            foreach (var element in ann)
            {
                link.AnnouncementsList.Add(element);
            }
            db.AnnouncementWithItsComments = link;
            db.SaveChanges();
        }

        public void AllAnnouncementsToUsers()
        {
            //Announcement announcement = db.Announcements.Find(id);

            ApplicationUser[] listOfUsers = db.Users.ToArray();

            List<ApplicationUser> allStudentList = new List<ApplicationUser>();
            List<Announcement> allAnnouncements = db.Announcements.ToList();
            //ViewStudent student = new ViewStudent();

            List<LinkAnnouncementAndStudent> AllStudentsAndAnnouncements = db.LinkAnnouncementAndStudents.ToList();


            for (int i = 0; i < listOfUsers.Length; i++)
            {
                //only add students
                if (listOfUsers[i].Roles.Count == 0)
                {
                    //allStudentList.StudentUsers.Add(listOfUsers[i].UserName)

                    //student.StudentUser = listOfUsers[i];
                    allStudentList.Add(listOfUsers[i]);
                }
            }
            bool studentAndAnnouncementAlreadyIn = false;
            //make sure students + announcementes aready in the model that links them already
            foreach (var student in allStudentList)
            {
                foreach (var singleAnnouncement in allAnnouncements)
                {
                    
                    
                    foreach (var content in AllStudentsAndAnnouncements)  //SpecificStudent.Id && singleAnnouncement.Id != link.SpecificAnnouncement.Id)
                    {
                        //check if the combination of student and announcement is already in the list of all students to all announcements
                        if ((content.SpecificStudent.Id == student.Id && content.SpecificAnnouncement.Id == singleAnnouncement.Id)) {
                            studentAndAnnouncementAlreadyIn = true;
                        }
                    }

                    //if the combination is not in all combinations then add it
                    if (!(studentAndAnnouncementAlreadyIn)) { 
                    
                        //store data in instance of the model
                        LinkAnnouncementAndStudent newStudentAnnouncementLink = new LinkAnnouncementAndStudent();
                        
                        newStudentAnnouncementLink.SpecificAnnouncement = singleAnnouncement;
                        newStudentAnnouncementLink.SpecificStudent = student;

                        //newStudentAnnouncementLink.SpecificStudentId = Convert.ToInt32(newStudentAnnouncementLink.SpecificStudent.Id);
                        newStudentAnnouncementLink.SpecificAnnouncementId = newStudentAnnouncementLink.SpecificAnnouncement.Id;

                        //add instance to database
                        db.LinkAnnouncementAndStudents.Add(newStudentAnnouncementLink);
                        db.SaveChanges();

                        studentAndAnnouncementAlreadyIn = false;
                    }
                 }
             }
         }
        public void SeenAnnouncement() {
            //get current user
            string currentUserID = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserID);

            //if current user is student
            if (currentUser.Roles.Count == 0)
            {


                ApplicationUser[] listOfUsers = db.Users.ToArray();

                //List<ApplicationUser> allStudentList = new List<ApplicationUser>();
                List<Announcement> allAnnouncements = db.Announcements.ToList();
                //ViewStudent student = new ViewStudent();

                //List<StudentNotViewed> linkStudentAndAnnouncements = db.StudentNotVieweds.ToList();

                List<StudentViewed> allAnnouncementsAndStudentsViewed = db.StudentVieweds.ToList();


                bool alreadySeenAnnouncements = false;
                //make sure students + announcementes already in the model that links them already
                
                foreach (var announcement in allAnnouncements)
                {
                    foreach (var studentAndAnnouncement in allAnnouncementsAndStudentsViewed)
                    {
                        //check that the current student and any announcement is not already in it, if so add it to seen
                        if ((studentAndAnnouncement.SpecificAnnouncement.Id == announcement.Id && studentAndAnnouncement.SpecificStudent.Id == currentUser.Id))
                        {
                            alreadySeenAnnouncements = true;
                        }
                            

                    }

                    if (!(alreadySeenAnnouncements))
                    {
                        StudentViewed newStudentAnnouncementLink = new StudentViewed();
                        //newStudentAnnouncementLink.SpecificStudentId = int.Parse(currentUser.Id);
                        newStudentAnnouncementLink.SpecificAnnouncementId = announcement.Id;
                        newStudentAnnouncementLink.SpecificAnnouncement = announcement;
                        newStudentAnnouncementLink.SpecificStudent = currentUser;

                        //add instance to database
                        db.StudentVieweds.Add(newStudentAnnouncementLink);
                        db.SaveChanges();

                        //alreadySeenAnnouncements = true
                    }

                    //store data in instance of the model

                }

            }
       }

        public void NotSeenAnnouncement()
        {
            List<LinkAnnouncementAndStudent> allStudentsToAllAnnouncements = db.LinkAnnouncementAndStudents.ToList();
            List<StudentViewed> allSeenStudentsToAnnouncements = db.StudentVieweds.ToList();
            List<StudentNotViewed> allNotSeenStudentsToAnnouncements = db.StudentNotVieweds.ToList();
            //List<StudentNotViewed>

            bool studentSeen = false; 
            foreach (var studentToAnnouncement in allStudentsToAllAnnouncements)
            {
                //if(allSeenStudentsToAnnouncements.Contains<SpecificStudent>(studentToAnnouncement.SpecificStudent) )
                foreach (var seenStudentToAnnouncement in allSeenStudentsToAnnouncements)
                {
                    //check if student to announcement is not already in the seen model
                    if(studentToAnnouncement.SpecificStudent.Id == seenStudentToAnnouncement.SpecificStudent.Id && studentToAnnouncement.SpecificAnnouncement.Id == seenStudentToAnnouncement.SpecificAnnouncement.Id)
                    {
                        studentSeen = true;
                    }
                    
                }
                if (!(studentSeen))
                {
                    bool alreadyInNotSeen = false;
                    //check not already in db of not seen
                    foreach(var studentNotSeen in allNotSeenStudentsToAnnouncements)
                    {
                        if(studentNotSeen.SpecificStudent.Id == studentToAnnouncement.SpecificStudent.Id && studentNotSeen.SpecificAnnouncement.Id == studentToAnnouncement.SpecificAnnouncement.Id)
                        {
                             alreadyInNotSeen = true;
                        }
                    }
                    if (!(alreadyInNotSeen)) {
                        //NEEDS TO HOLD EVERY STUDENT TO EVERY ANNOUNCEMENT NOT SEEN

                        StudentNotViewed newStudentAnnouncementLink = new StudentNotViewed();
                        //newStudentAnnouncementLink.SpecificStudentId = int.Parse(currentUser.Id);
                        newStudentAnnouncementLink.SpecificAnnouncementId = studentToAnnouncement.SpecificAnnouncement.Id;
                        newStudentAnnouncementLink.SpecificAnnouncement = studentToAnnouncement.SpecificAnnouncement;
                        newStudentAnnouncementLink.SpecificStudent = studentToAnnouncement.SpecificStudent;
                        db.StudentNotVieweds.Add(newStudentAnnouncementLink);
                        db.SaveChanges();
                        //add instance to database


                        studentSeen = false;

                        
                    }
                   
                }
                
            }
            List<StudentViewed> allSeenStudentsToAnnouncementsUpdated = db.StudentVieweds.ToList();
            List<StudentNotViewed> allNotSeenStudentsToAnnouncementsUpdated = db.StudentNotVieweds.ToList();
            //need to remove students from the not seen list that are in the seen list
            bool studentInBoth = false;
            bool skipNext = false;
            foreach (var studentNotSeenAnnouncement in allNotSeenStudentsToAnnouncementsUpdated.ToList())
            {
                foreach(var studentSeenAnnouncement in allSeenStudentsToAnnouncementsUpdated)
                {
                    if (!skipNext) {
                        skipNext = false;
                        if (studentSeenAnnouncement.SpecificStudent.Id == studentNotSeenAnnouncement.SpecificStudent.Id && studentSeenAnnouncement.SpecificAnnouncement.Id == studentNotSeenAnnouncement.SpecificAnnouncement.Id)
                        {
                            
                            db.StudentNotVieweds.Remove(studentNotSeenAnnouncement);
                            db.SaveChanges();
                            allNotSeenStudentsToAnnouncementsUpdated.Remove(studentNotSeenAnnouncement);
                            skipNext = true;
                            //allNotSeenStudentsToAnnouncementsUpdated = 
                        }
                    }
                    
                }
               
            }
           
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxInsertComment(int? id, AnnouncementWithItsComments commentInstance)
        {
            //finds announcement that comment should be placed on
            Announcement announcement = db.Announcements.Find(id);

            Comment comment = new Comment();
            comment.commentContent = commentInstance.CommentModel.commentContent;

            //find who sent the comment
            string currentUserID = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserID);
            announcement.staffName = currentUser.UserName;
            comment.userName = currentUser.UserName;

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
        public ActionResult AjaxCreateAnnouncement([Bind(Include = "Id,announcementTitle,announcementContent")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.announcementTimeStamp = DateTime.Now.ToString("h:mm:ss tt");

                //find out who posted announcement
                string currentUserID = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserID);
                announcement.staffName = currentUser.UserName;

                //get all users
                ApplicationUser[] listOfUsers = db.Users.ToArray();

                //views
                announcement.WhoNotViewed = new List<string>();
                for (int counter = 0; counter < listOfUsers.Length; counter++) {

                    //find all students
                    //if (!(User.IsInRole("canEdit")))
                    if((listOfUsers[counter].Roles.Count == 0))
                    {
                        
                        announcement.WhoNotViewed.Add(listOfUsers[counter].UserName);
                    }
                }
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
