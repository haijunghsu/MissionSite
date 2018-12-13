/*
 Team 1-3: HaiJung Hsu, Tiara Johnson, Bailey Coleman, and Ethan Guinn
 IS 403 Project 2 Description: This web solution contains a mission site 
        where users with login credentials can view frequently asked 
        questions about specific missions, post new questions, and answer 
        questions posted by other users.
 12/11/2018
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MissionSite.DAL;
using MissionSite.Models;


namespace MissionSite.Controllers
{
    public class HomeController : Controller
    {
        private MissionSiteContext db = new MissionSiteContext();
        public static int MissionIDPointer = 0;
        public static int UserIDPointer = 0;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        { 
            return View();
        }
        
        public ActionResult SelectMission()
        {
            ViewBag.MissionID = new SelectList(db.Missions, "MissionID", "MissionName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectMission([Bind(Include = "MissionID")] MissionQuestions missionQuestions)
        {
            if (ModelState.IsValid)
            {
                MissionIDPointer = missionQuestions.MissionID;
                return RedirectToAction("MissionFAQ", new { id = UserIDPointer});
            }

            return View();
        }

        [Authorize]
        public ActionResult MissionFAQ(int id)
        {
            UserIDPointer = id;
            //if user login first without selecting the mission, it will take user back to index
            if(MissionIDPointer == 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.MissionInfo = db.Missions.Find(MissionIDPointer);
            var missionQuestions = db.MissionQuestions.Include(m => m.Missions).Include(m => m.Users);
            ViewBag.Qs = missionQuestions;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddAnswer(string answer, int QID)
        {
            db.MissionQuestions.Find(QID).MissionAnswer = answer;
            db.MissionQuestions.Find(QID).UserID = UserIDPointer;
            db.SaveChanges();
            return RedirectToAction("MissionFAQ", new { id = UserIDPointer });
        }

        [Authorize]
        public ActionResult AddQuestion(string question, int mID)
        {
            MissionQuestions newQ = new MissionQuestions();
            newQ.MissionID = mID;
            newQ.MissionQuestion = question;
            newQ.UserID = UserIDPointer;
            db.MissionQuestions.Add(newQ);
            db.SaveChanges();
            return RedirectToAction("MissionFAQ", new { id = UserIDPointer });
        }
    }
}