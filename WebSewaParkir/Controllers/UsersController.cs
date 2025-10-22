using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSewaParkir.Data;
using WebSewaParkir.Models;

namespace WebSewaParkir.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        private SewaParkirContext db = new SewaParkirContext();

        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return Content("Jumlah data Users: " + users.Count);
        }
    }
}