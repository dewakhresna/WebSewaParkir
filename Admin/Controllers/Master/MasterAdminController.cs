using KandangMobil.Filters;
using KandangMobil.Helpers;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.Master
{
    [AdminAuthorize]
    public class MasterAdminController : Controller
    {
        private readonly IMasterAdmin _IMasterAdmin;
        private readonly UploadHelper _upload;
        public MasterAdminController(IMasterAdmin iMasterAdmin, UploadHelper upload)
        {
            _IMasterAdmin = iMasterAdmin;
            _upload = upload;
        }
        public async Task<IActionResult> Index()
        {
            int? adminId = HttpContext.Session.GetInt32("AdminId");

            if (adminId == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var admins = await _IMasterAdmin.Find(adminId.Value);
            return View(admins);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(MasterAdminModel data, IFormFile photo)
        {
            int? adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Index", "Auth");

            var admin = await _IMasterAdmin.Find(adminId.Value);

            admin.Username = data.Username;
            admin.Email = data.Email;

            if (photo != null && photo.Length > 0)
            {
                string? newFileName = await _upload.UploadFile(photo, "uploads/admin/profile/");

                if (!string.IsNullOrEmpty(newFileName))
                {
                    if (!string.IsNullOrEmpty(admin.Photo))
                    {
                        _upload.DeleteFile("uploads/admin/profile/", admin.Photo);
                    }

                    admin.Photo = newFileName;
                }
            }

            await _IMasterAdmin.UpdateProfile(admin);

            TempData["Success"] = "Profil berhasil diperbarui!";
            return RedirectToAction("Index", "MasterRental");
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(MasterAdminModel data)
        {
            int? adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Index", "Auth");

            var admin = await _IMasterAdmin.Find(adminId.Value);

            string currentHash = HashHelper.ToSha256(data.CurrentPassword);

            if (admin.Password != currentHash)
            {
                TempData["Error"] = "Password lama tidak sesuai.";
                return RedirectToAction("Index", "MasterAdmin");
            }

            if (data.NewPassword != data.ConfirmPassword)
            {
                TempData["Error"] = "Konfirmasi password tidak cocok.";
                return RedirectToAction("Index", "MasterAdmin");
            }

            if (data.NewPassword.Length < 6)
            {
                TempData["Error"] = "Password baru minimal 6 karakter.";
                return RedirectToAction("Index", "MasterAdmin");
            }

            admin.Password = HashHelper.ToSha256(data.NewPassword);
            await _IMasterAdmin.UpdatePassword(admin);

            TempData["Success"] = "Password berhasil diperbarui!";
            return RedirectToAction("Index", "MasterRental");
        }

    }
}
