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
            var rentals = db.CarRentals.ToList();
            return View(rentals);
        }
        [HttpGet]
        public ActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCar(CarRental car)
        {
            if (ModelState.IsValid)
            {
                db.CarRentals.Add(car);
                db.SaveChanges();
                return RedirectToAction("CarRental");
            }
            return View(car);
        }
        public ActionResult EditCar()
        {
            return View();
        }
        public ActionResult User()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("User");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditUser(User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingUser = db.Users.Find(user.Id);
        //        if (existingUser == null)
        //            return HttpNotFound();

        //        // Update field yang boleh diubah
        //        existingUser.Name = user.Name;
        //        existingUser.Email = user.Email;
        //        existingUser.Telp = user.Telp;

        //        // Kalau password diisi, baru ganti
        //        if (string.IsNullOrEmpty(user.Password))
        //        {
        //            var oldUser = db.Users.Find(user.Id);
        //            existingUser.Password = oldUser.Password; // ambil dari database
        //        }
        //        else
        //        {
        //            existingUser.Password = user.Password;
        //        }

        //        db.SaveChanges();
        //        return RedirectToAction("User");
        //    }

        //    return View(user);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("User");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("User");
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
                return HttpNotFound();

            db.Users.Remove(user);
            db.SaveChanges();

            return RedirectToAction("User"); // kembali ke daftar user
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