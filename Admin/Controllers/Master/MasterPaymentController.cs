using KandangMobil.Helpers;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.Master
{
    public class MasterPaymentController : Controller
    {
        private readonly IMasterRental _IMasterRental;
        private readonly IMasterKendaraan _IMasterKendaraan;
        private readonly IMasterUser _IMasterUser;
        private readonly IMasterSubscriptions _IMasterSubscriptions;
        private readonly IMasterParkirSlot _IMasterParkirSlot;
        private readonly UploadHelper _upload;
        public MasterPaymentController( IMasterRental iMasterRental, IMasterKendaraan iMasterKendaraan, IMasterUser iMasterUser, IMasterSubscriptions iMasterSubscriptions, IMasterParkirSlot iMasterParkirSlot, UploadHelper upload)
        {
            _IMasterRental = iMasterRental;
            _IMasterKendaraan = iMasterKendaraan;
            _IMasterUser = iMasterUser;
            _IMasterSubscriptions = iMasterSubscriptions;
            _upload = upload;
            _IMasterParkirSlot = iMasterParkirSlot;
        }
        public async Task<IActionResult> Index()
        {
            int? adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Index", "AuthAdmin");

            var payments = await _IMasterSubscriptions.Get();
            foreach (var p in payments)
            {
                p.Car = await _IMasterRental.Find(p.CarRentalId);
                p.ParkirSlot = await _IMasterParkirSlot.Find(p.ParkirSlotId);
            }

            return View(payments);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            int? adminId = HttpContext.Session.GetInt32("AdminId");
            if (adminId == null)
                return RedirectToAction("Index", "AuthAdmin");
            var payments = await _IMasterSubscriptions.Find(Id);
            ViewBag.User = await _IMasterUser.Find(payments.UserId);
            ViewBag.Rental = await _IMasterRental.Find(payments.CarRentalId);
            return View(payments);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmTransaction(MasterSubscriptionsModel data)
        {
            await _IMasterSubscriptions.ConfirmTransaction(data);
            return RedirectToAction("Index");
        }
    }
}
