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

            var payments = await _IMasterSubscriptions.FindByUser(userId.Value);

            foreach (var p in payments)
            {
                p.Car = await _IMasterRental.Find(p.CarRentalId);
                p.Car.Kendaraan = await _IMasterKendaraan.Find(p.Car.IdKendaraan);
                p.ParkirSlot = await _IMasterParkirSlot.Find(p.ParkirSlotId);
            }

            return View(payments);
        }

        public async Task<IActionResult> Details(int Id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "AuthUser");

            var payment = await _IMasterSubscriptions.Find(Id);
            var rental = await _IMasterRental.Find(payment.CarRentalId);
            var kendaraan = await _IMasterKendaraan.Find(rental.IdKendaraan);
            var user = await _IMasterUser.Find(userId.Value);

            var vm = new PaymentDetailViewModel
            {
                Payment = payment,
                Rental = rental,
                Kendaraan = kendaraan,
                User = user
            };

            return View(vm);
        }
    }
}
