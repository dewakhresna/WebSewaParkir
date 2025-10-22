using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSewaParkir.Data;
using WebSewaParkir.Models;

namespace WebSewaParkir.Controllers
{
    public class AdminController : Controller
    {
        private SewaParkirContext db = new SewaParkirContext();
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult User()
        {
            //var users = db.Users.ToList();
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }
        public ActionResult EditUser()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

    }
}