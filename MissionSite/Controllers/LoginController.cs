using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MissionSite.DAL;
using MissionSite.Models;

namespace MissionSite.Controllers
{
    public class LoginController : Controller
    {
        private MissionSiteContext db = new MissionSiteContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form, bool rememberMe = false)
        {
            String email = form["Email address"].ToString();
            String password = form["Password"].ToString();
            int id = 0;

            var currentUser = db.Database.SqlQuery<Users>(
            "Select * " +
            "FROM Users " +
            "WHERE UserEmail = '" + email + "' AND " +
            "UserPassword = '" + password + "'");

            /*IEnumerable<Users> currentUser=
            db.Database.SqlQuery <Users>(
            "Select * " +
            "FROM Users " +
            "WHERE UserEmail = '" + email + "' AND " +
            "UserPassword = '" + password + "'");*/


            if (currentUser.Count() > 0)
            {
                foreach(var user in db.Users)
                {
                    if (user.UserEmail == email)
                    {
                        id = user.UserID;
                    }
                }

                FormsAuthentication.SetAuthCookie(email, rememberMe);
                return RedirectToAction("Index", "Home", new { userlogin = email, id = id});
            }
            else
            {
                return View();
            }
        }

        public ActionResult Register()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UserID,UserEmail,UserPassword,UserFirstName,UserLastName")] Users users, bool rememberMe = false)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                string email = users.UserEmail;
                int id = users.UserID;
                FormsAuthentication.SetAuthCookie(email, rememberMe);
                return RedirectToAction("Index", "Home", new { userlogin = email, id = id });
            }

            return View(users);
        }
    }
}