using KandangMobil.Filters;
using KandangMobil.Helpers;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.User
{
    [UserAuthorize]
    public class UserProfileController : Controller
    {
        private readonly IMasterUser _IMasterUser;
        private readonly UploadHelper _upload;
        public UserProfileController(IMasterUser iMasterUser, UploadHelper upload)
        {
            _IMasterUser = iMasterUser;
            _upload = upload;
        }
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "AuthUser");

            var users = await _IMasterUser.Find(userId.Value);
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(MasterUserModel data, IFormFile photo)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Auth");

            var user = await _IMasterUser.Find(userId.Value);

            user.Name = data.Name;
            user.Email = data.Email;
            user.Telp = data.Telp;

            if (photo != null && photo.Length > 0)
            {
                string? newFileName = await _upload.UploadFile(photo, "uploads/user/profile/");

                if (!string.IsNullOrEmpty(newFileName))
                {
                    if (!string.IsNullOrEmpty(user.Photo))
                    {
                        _upload.DeleteFile("uploads/user/profile/", user.Photo);
                    }

                    user.Photo = newFileName;
                }
            }

            await _IMasterUser.UpdateProfile(user);

            TempData["Success"] = "Profil berhasil diperbarui!";
            return RedirectToAction("Index", "UserRental");
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(MasterUserModel data)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Auth");

            var user = await _IMasterUser.Find(userId.Value);

            string currentHash = HashHelper.ToSha256(data.CurrentPassword);

            if (user.Password != currentHash)
            {
                TempData["Error"] = "Password lama tidak sesuai.";
                return RedirectToAction("Index", "UserProfile");
            }

            if (data.NewPassword != data.PasswordConfirmation)
            {
                TempData["Error"] = "Konfirmasi password tidak cocok.";
                return RedirectToAction("Index", "UserProfile");
            }

            if (data.NewPassword.Length < 6)
            {
                TempData["Error"] = "Password baru minimal 6 karakter.";
                return RedirectToAction("Index", "UserProfile");
            }

            user.Password = HashHelper.ToSha256(data.NewPassword);
            await _IMasterUser.UpdatePassword(user);

            TempData["Success"] = "Password berhasil diperbarui!";
            return RedirectToAction("Index", "UserRental");
        }

    }
}
