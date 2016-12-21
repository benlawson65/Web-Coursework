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
            return View(announcement); //View(db.Announcements.ToList());
        }
        public void InsertsAnnouncements()
        {
            AnnouncementWithItsComment link = new AnnouncementWithItsComment();

            //get all students and all announcements
            AllAnnouncementsToUsers();

            //get all students that have seen announcements
            SeenAnnouncement();

            //find students that haven't viewed an announcement
            NotSeenAnnouncement();

            //update the announcements and comments view model
            ICollection<Announcement> ann = db.Announcements.ToList();
            link.AnnouncementsList = new List<Announcement>();
            foreach (var element in ann)
            {
                link.AnnouncementsList.Add(element);
            }
            db.AnnouncementWithItsComment = link;
            db.SaveChanges();
        }

        //get all announcements to all students
        public void AllAnnouncementsToUsers()
        {

            //get list of all users
            ApplicationUser[] listOfUsers = db.Users.ToArray();

            //access all students and announcements
            List<ApplicationUser> allStudentList = new List<ApplicationUser>();
            List<Announcement> allAnnouncements = db.Announcements.ToList();

            List<LinkAnnouncementAndStudent> allStudentsAndAnnouncements = db.LinkAnnouncementAndStudents.ToList();

            //find only people that are students
            for (int i = 0; i < listOfUsers.Length; i++)
            {
                //only add students
                if (listOfUsers[i].Roles.Count == 0)
                {

                    allStudentList.Add(listOfUsers[i]);
                }
            }

            bool studentAndAnnouncementAlreadyIn = false;
            //make sure students + announcementes already in the model that links them
            foreach (var student in allStudentList)
            {
                foreach (var singleAnnouncement in allAnnouncements)
                {

                    //add only things not already in it
                    
                        foreach (var content in allStudentsAndAnnouncements)
                        {
                        //studentAndAnnouncementAlreadyIn = false;

                        //check if the combination of student and announcement is already in the list of all students to all announcements
                        if ((content.SpecificStudent.Id == student.Id && content.SpecificAnnouncement.Id == singleAnnouncement.Id))
                            {

                                studentAndAnnouncementAlreadyIn = true;
                            }
                           
                        }
                    if (!studentAndAnnouncementAlreadyIn)
                    {
                        //store data in instance of the model
                        LinkAnnouncementAndStudent newStudentAnnouncementLink = new LinkAnnouncementAndStudent();

                        newStudentAnnouncementLink.SpecificAnnouncement = singleAnnouncement;
                        newStudentAnnouncementLink.SpecificStudent = student;

                        //newStudentAnnouncementLink.SpecificStudentId = Convert.ToInt32(newStudentAnnouncementLink.SpecificStudent.Id);
                        newStudentAnnouncementLink.SpecificAnnouncementId = newStudentAnnouncementLink.SpecificAnnouncement.Id;

                        //add instance to database
                        db.LinkAnnouncementAndStudents.Add(newStudentAnnouncementLink);
                        db.SaveChanges();

                        
                    }
                    studentAndAnnouncementAlreadyIn = false;
                }
             }
         }

        //get all students that have specific announcements 
        public void SeenAnnouncement() {
            //get current user
            string currentUserID = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserID);

            //if current user is student
            if (currentUser.Roles.Count == 0)
            {

                //get list of users, announcements and all students pointing to all announcements
                ApplicationUser[] listOfUsers = db.Users.ToArray();
                List<Announcement> allAnnouncements = db.Announcements.ToList();
                List<StudentViewed> allAnnouncementsAndStudentsViewed = db.StudentVieweds.ToList();

                bool alreadySeenAnnouncements = false;
                
                //check if students + announcementes already in the seen model, if not, add them to it
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
                        //create new instance of student to announcement
                        StudentViewed newStudentAnnouncementLink = new StudentViewed();
                        newStudentAnnouncementLink.SpecificAnnouncementId = announcement.Id;
                        newStudentAnnouncementLink.SpecificAnnouncement = announcement;
                        newStudentAnnouncementLink.SpecificStudent = currentUser;

                        //add instance to database
                        db.StudentVieweds.Add(newStudentAnnouncementLink);
                        db.SaveChanges();
                    }

                }

            }
       }

        //students that have not seen specific announcements
        public void NotSeenAnnouncement()
        {

            //get all students to all announcements, all seen announcements and current not seen anouncements
            List<LinkAnnouncementAndStudent> allStudentsToAllAnnouncements = db.LinkAnnouncementAndStudents.ToList();
            List<StudentViewed> allSeenStudentsToAnnouncements = db.StudentVieweds.ToList();
            List<StudentNotViewed> allNotSeenStudentsToAnnouncements = db.StudentNotVieweds.ToList();
            List<StudentViewed> convertedAllList = new List<StudentViewed>();
            List<StudentViewed> convertedNotSeen = new List<StudentViewed>();
            List<StudentNotViewed> correctNotSeen = new List<StudentNotViewed>();

            //put all students to announcements list in the same data type as seen announcements
            foreach (var allContent in allStudentsToAllAnnouncements)
            {
                StudentViewed newConvertedContent = new StudentViewed();
                newConvertedContent.SpecificAnnouncementId = allContent.SpecificAnnouncement.Id;
                newConvertedContent.SpecificAnnouncement = allContent.SpecificAnnouncement;
                newConvertedContent.SpecificStudent = allContent.SpecificStudent;
                convertedAllList.Add(newConvertedContent);

                
            }

            //put all not seen students in the same data type as seen announcements
            foreach (var allNotContent in allNotSeenStudentsToAnnouncements)
            {
                StudentViewed newConvertedContent = new StudentViewed();
                newConvertedContent.SpecificAnnouncementId = allNotContent.SpecificAnnouncement.Id;
                newConvertedContent.SpecificAnnouncement = allNotContent.SpecificAnnouncement;
                newConvertedContent.SpecificStudent = allNotContent.SpecificStudent;
                convertedNotSeen.Add(newConvertedContent);
             }

            //take seen students away from all seen students and put result in not seeen
            convertedNotSeen = convertedAllList.Except(allSeenStudentsToAnnouncements).ToList();

            //convert not seen back to model data type
            foreach (var allNotContent in convertedNotSeen)
            {
                StudentNotViewed newConvertedContent = new StudentNotViewed();
                newConvertedContent.SpecificAnnouncementId = allNotContent.SpecificAnnouncement.Id;
                newConvertedContent.SpecificAnnouncement = allNotContent.SpecificAnnouncement;
                newConvertedContent.SpecificStudent = allNotContent.SpecificStudent;
                correctNotSeen.Add(newConvertedContent);
            }

            //remove all current elements
            foreach (var notSeen in db.StudentNotVieweds)
            {
                db.StudentNotVieweds.Remove(notSeen);
            }
            db.SaveChanges();
            //add correct elements
            foreach (var correctContent in correctNotSeen)
            {
                db.StudentNotVieweds.Add(correctContent);
            }

            /*

            //check if any of the students to announcements have not already been seen
            bool studentSeen = false; 
            foreach (var studentToAnnouncement in allStudentsToAllAnnouncements)
            {
                foreach (var seenStudentToAnnouncement in allSeenStudentsToAnnouncements)
                {
                    //check if student to announcement is not already in the seen model
                    if(studentToAnnouncement.SpecificStudent.Id == seenStudentToAnnouncement.SpecificStudent.Id && studentToAnnouncement.SpecificAnnouncement.Id == seenStudentToAnnouncement.SpecificAnnouncement.Id)
                    {
                        studentSeen = true;
                    }
                    
                }

                //if the student hasnt seen the announcement, check its not already in the not seen model
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

                    //if the student hasn't already not seen the announcement add it to not seen
                    if (!(alreadyInNotSeen))
                    {

                        StudentNotViewed newStudentAnnouncementLink = new StudentNotViewed();
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

            //if a student has now seen an announcement, remove them from not seen
            List<StudentViewed> allSeenStudentsToAnnouncementsUpdated = db.StudentVieweds.ToList();
            List<StudentNotViewed> allNotSeenStudentsToAnnouncementsUpdated = db.StudentNotVieweds.ToList();
            //need to remove students from the not seen list that are in the seen list
            bool studentInBoth = false;
            bool skipNext = false;
            foreach (var studentNotSeenAnnouncement in allNotSeenStudentsToAnnouncementsUpdated.ToList())
            {
                foreach(var studentSeenAnnouncement in allSeenStudentsToAnnouncementsUpdated)
                {
                    if (!skipNext)
                    {
                        skipNext = false;
                        if (studentSeenAnnouncement.SpecificStudent.Id == studentNotSeenAnnouncement.SpecificStudent.Id && studentSeenAnnouncement.SpecificAnnouncement.Id == studentNotSeenAnnouncement.SpecificAnnouncement.Id)
                        {
                            
                            db.StudentNotVieweds.Remove(studentNotSeenAnnouncement);
                            db.SaveChanges();
                            allNotSeenStudentsToAnnouncementsUpdated.Remove(studentNotSeenAnnouncement);
                            skipNext = true;
                        }
                    }
                    
                }
               
            }
           */
            
        }

        //adds comment on same page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxInsertComment(int? id, [Bind(Include = "CommentModel")] AnnouncementWithItsComment commentInstance)
        {
            //finds announcement that comment should be placed on
            Announcement announcement = db.Announcements.Find(id);

            Comment comment = new Comment();
            comment.CommentContent = commentInstance.CommentModel.CommentContent;

            //find who sent the comment
            string currentUserID = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserID);
            announcement.staffName = currentUser.UserName;
            comment.UserName = currentUser.UserName;

            //set time the comment was placed
            comment.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");
            announcement.ListOfComments.Add(comment);

            //add data to binded view model
            InsertsAnnouncements();

            //add all data to database
            db.SaveChanges();
            //ModelState.Clear();

            return PartialView("_Announcement",db.AnnouncementWithItsComment);
        }
        public ActionResult BuildAnnouncements()
        {
            InsertsAnnouncements();
            return PartialView("_Announcement",db.AnnouncementWithItsComment);
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
        public ActionResult Create([Bind(Include = "Id,Title,Content,annoucmementTimeStamp,staffName,staffName_Id")] Announcement announcement)
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
        public ActionResult AjaxCreateAnnouncement([Bind(Include = "Id,Title,Content")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                announcement.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");

                //find out who posted announcement
                string currentUserID = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserID);
                announcement.staffName = currentUser.UserName;

                //get all users
                ApplicationUser[] listOfUsers = db.Users.ToArray();

                //views
                announcement.WhoNotViewed = new List<string>();
                for (int counter = 0; counter < listOfUsers.Length; counter++)
                {

                    //find all students
                    if((listOfUsers[counter].Roles.Count == 0))
                    {
                        announcement.WhoNotViewed.Add(listOfUsers[counter].UserName);
                    }
                }
                announcement.ListOfComments = new List<Comment>();
                db.Announcements.Add(announcement);
                db.SaveChanges();
            }

            //returning the set of announcements (which is a partial view) with added anouncement
            return BuildAnnouncements();
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
        public ActionResult Edit([Bind(Include = "Id,Title,Content,annoucmementTimeStamp,staffName,staffName_Id")] Announcement announcement)
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

        //delete comment on same page without refresh
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxDeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //find specified comment
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            //update database and comments + announcements view model
            db.Comments.Remove(comment);
            db.SaveChanges();
            InsertsAnnouncements();

            db.SaveChanges();
            return PartialView("_Announcement", db.AnnouncementWithItsComment); ;
        }

        //deletes announcement on same page without needing to refresh the page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjaxDeleteAnnouncement(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //find specified announcement and comments belonging to it
            Announcement announcement = db.Announcements.Find(id);
            ICollection<Comment> allComments = db.Comments.ToList();

            List<Comment> commentsList = new List<Comment>();

            //delete all comments for that announcement
            foreach (var element in announcement.ListOfComments)
            {
               //db.Comments.Remove(db.Comments.Find(element.Id));
                commentsList.Add(element);
            }

            
            foreach (var element1 in commentsList)
            {
                db.Comments.Remove(element1);
            }
            
            //remove announcement and update comment + announcement partial view
            db.SaveChanges();
            db.Announcements.Remove(announcement);
            
            db.SaveChanges();
            InsertsAnnouncements();
            return PartialView("_Announcement", db.AnnouncementWithItsComment);
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
