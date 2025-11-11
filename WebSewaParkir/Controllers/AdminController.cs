using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSewaParkir.Data;
using WebSewaParkir.Models;
using WebSewaParkir.Filters;

namespace WebSewaParkir.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private SewaParkirContext db = new SewaParkirContext();

        public ActionResult Dashboard()
        {
            var cars = db.CarRentals.ToList();
            return View(cars);
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
                return RedirectToAction("dashboard");
            }

            //select * from master
            //orm

            return View(car);
        }

        [HttpGet]
        public ActionResult EditCar(int id)
        {
            var car = db.CarRentals.Find(id);
            if (car == null)
                return HttpNotFound();
            return View(car);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(CarRental car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("dashboard");
            }
            return View(car);
        }

        [HttpGet]
        public ActionResult DeleteCar(int id)
        {
            var car = db.CarRentals.Find(id);
            if (car != null)
            {
                db.CarRentals.Remove(car);
                db.SaveChanges();
            }
            return RedirectToAction("dashboard");
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
        [HttpGet]
        public ActionResult Profile()
        {
            int adminId = Convert.ToInt32(Session["AdminId"]);
            var admin = db.Admins.Find(adminId);
            if (admin == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult Edit(Admin model, HttpPostedFileBase photo)
        {
            int adminId = (int)Session["AdminId"];
            var admin = db.Admins.Find(adminId);

            if (admin == null)
                return HttpNotFound();

            if (ModelState.IsValid)
            {
                // Update data dasar
                admin.Username = model.Username;
                admin.Email = model.Email;

                // Jika ada upload foto baru
                if (photo != null && photo.ContentLength > 0)
                {
                    // Simpan dengan nama unik (misalnya pakai timestamp)
                    string fileName = DateTime.Now.ToString("yyyyMMdd") + "_" + System.IO.Path.GetFileName(photo.FileName);
                    string path = Server.MapPath("~/Uploads/Admin/Profile/" + fileName);

                    // Pastikan folder ada
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/Uploads/Admin/Profile/"));

                    // Simpan file ke folder
                    photo.SaveAs(path);

                    // Hapus foto lama jika ada
                    if (!string.IsNullOrEmpty(admin.Photo))
                    {
                        string oldPath = Server.MapPath("~/Uploads/Admin/Profile/" + admin.Photo);
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    // Update nama file di database
                    admin.Photo = fileName;
                }

                db.SaveChanges();


                TempData["Success"] = "Profil berhasil diperbarui!";
                return RedirectToAction("Profile");
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string current_password, string new_password, string new_password_confirmation)
        {
            int adminId = (int)Session["AdminId"];
            var admin = db.Admins.Find(adminId);
            if (admin == null)
            {
                TempData["Error"] = "Data admin tidak ditemukan.";
                return RedirectToAction("Profile");
            }

            // 3️ Cek apakah password lama cocok
            if (admin.Password != current_password)
            {
                TempData["Error"] = "Password lama salah!";
                return RedirectToAction("Profile");
            }

            // 4️⃣ Cek konfirmasi password baru
            if (new_password != new_password_confirmation)
            {
                TempData["Error"] = "Konfirmasi password tidak cocok!";
                return RedirectToAction("Profile");
            }

            // 5️⃣ Simpan password baru (saat ini masih plain text, nanti bisa di-hash)
            admin.Password = new_password;
            db.SaveChanges();

            TempData["Success"] = "Password berhasil diubah.";
            return RedirectToAction("Profile");
        }
    }
}