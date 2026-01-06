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
                p.Car.Kendaraan = await _IMasterKendaraan.Find(p.Car.IdKendaraan);
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
            var payment = await _IMasterSubscriptions.Find(Id);
            var rental = await _IMasterRental.Find(payment.CarRentalId);
            var kendaraan = await _IMasterKendaraan.Find(rental.IdKendaraan);
            var user = await _IMasterUser.Find(payment.UserId);

            var vm = new PaymentDetailViewModel
            {
                Payment = payment,
                Rental = rental,
                Kendaraan = kendaraan,
                User = user
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmTransaction(PaymentDetailViewModel model)
        {
            if (model.Payment == null)
            {
                return RedirectToAction("Index");
            }

            int id = model.Payment.Id;
            int status = model.Payment.Status;

            if (status == 2)
            {
                await _IMasterSubscriptions.ConfirmTransaction(id, status);
            }
            else
            {
                await _IMasterSubscriptions.RejectTransaction(id, status);
            }
            return RedirectToAction("Index");
        }
    }
}
