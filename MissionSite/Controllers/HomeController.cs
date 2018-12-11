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
    [Authorize]
    public class HomeController : Controller
    {
        private MissionSiteContext db = new MissionSiteContext();
        public static int MissionIDPointer = 0;
        public static int UserIDPointer = 0;
        public static Queue<string> UKQ = new Queue<string>();
        public static Queue<string> HKQ = new Queue<string>();
        public static Queue<string> ODQ = new Queue<string>();

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
                return RedirectToAction("MissionFAQ");
            }

            return View();
        }

        public ActionResult MissionFAQ()
        {
            ViewBag.MissionInfo = db.Missions.Find(MissionIDPointer);
            var missionQuestions = db.MissionQuestions.Include(m => m.Missions).Include(m => m.Users);
            ViewBag.Qs = missionQuestions;
            return View();
        }

        [HttpPost]
        public ActionResult AddAnswer(string answer, int QID)
        {
            db.MissionQuestions.Find(QID).MissionAnswer = answer;
            db.SaveChanges();
            return RedirectToAction("MissionFAQ");
        }

        public ActionResult AddUKQ(string question)
        {
            UKQ.Enqueue(question);
            return RedirectToAction("LondonEngland");
        }

        public ActionResult AddHKQ(string question)
        {
            HKQ.Enqueue(question);
            return RedirectToAction("ChinaHongKong");
        }

        public ActionResult AddODQ(string question)
        {
            ODQ.Enqueue(question);
            return RedirectToAction("OgdenUtah");
        }

    }
}