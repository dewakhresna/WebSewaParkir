using KandangMobil.Helpers;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.User
{
    public class UserPaymentStatusController : Controller
    {
        private readonly IMasterRental _IMasterRental;
        private readonly IMasterKendaraan _IMasterKendaraan;
        private readonly IMasterUser _IMasterUser;
        private readonly IMasterSubscriptions _IMasterSubscriptions;
        private readonly IMasterParkirSlot _IMasterParkirSlot;
        private readonly UploadHelper _upload;
        public UserPaymentStatusController( IMasterRental iMasterRental, IMasterKendaraan iMasterKendaraan, IMasterUser iMasterUser, IMasterSubscriptions iMasterSubscriptions, IMasterParkirSlot iMasterParkirSlot, UploadHelper upload)
        {
            _IMasterRental = iMasterRental;
            _IMasterKendaraan = iMasterKendaraan;
            _IMasterUser = iMasterUser;
            _IMasterSubscriptions = iMasterSubscriptions;
            _upload = upload;
            _IMasterParkirSlot = iMasterParkirSlot;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "AuthUser");
            var payment = await _IMasterSubscriptions.Find(userId.Value);
            ViewBag.Car = await _IMasterRental.Find(payment.CarRentalId);
            ViewBag.ParkirSLot = await _IMasterParkirSlot.Find(payment.ParkirSlotId);
            return View(payment);
        }
    }
}
