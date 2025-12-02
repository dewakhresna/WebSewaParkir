using KandangMobil.Helpers;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Master;

namespace KandangMobil.Controllers.User
{
    public class UserSubscriptionsController : Controller
    {
        private readonly IMasterRental _IMasterRental;
        private readonly IMasterKendaraan _IMasterKendaraan;
        private readonly IMasterUser _IMasterUser;
        private readonly IMasterSubscriptions _IMasterSubscriptions;
        private readonly IMasterPrice _IMasterPrice;
        private readonly UploadHelper _upload;
        public UserSubscriptionsController( IMasterRental iMasterRental, IMasterKendaraan iMasterKendaraan, IMasterUser iMasterUser, IMasterSubscriptions iMasterSubscriptions, IMasterPrice iMasterPrice, UploadHelper upload)
        {
            _IMasterRental = iMasterRental;
            _IMasterKendaraan = iMasterKendaraan;
            _IMasterUser = iMasterUser;
            _IMasterSubscriptions = iMasterSubscriptions;
            _IMasterPrice = iMasterPrice;
            _upload = upload;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int Id)
        {
            ViewBag.Rental = await _IMasterRental.Find(Id);
            ViewBag.Price = await _IMasterPrice.Get();
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Add(MasterSubscriptionsModel data, IFormFile PaymentProof)
        //{
        //    int? userId = HttpContext.Session.GetInt32("UserId");

        //    if (userId == null)
        //        return RedirectToAction("Index", "AuthUser");

        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Rental = await _IMasterRental.Find(data.CarRentalId);
        //        return View("Index", data);
        //    }

        //    var user = await _IMasterUser.Find(userId.Value);

        //    data.UserId = user.Id;
        //    data.EndDate = DateTime.Now.AddMonths(data.Time);
        //    data.LastPaymentDate = DateTime.Now;
        //    data.Price = 300000;
        //    data.Status = 1;

        //    if (PaymentProof != null && PaymentProof.Length > 0)
        //    {
        //        string? newFileName = await _upload.UploadFile(PaymentProof, "uploads/user/paymentproof/");

        //        if (!string.IsNullOrEmpty(newFileName))
        //        {
        //            if (!string.IsNullOrEmpty(data.PaymentProof))
        //            {
        //                _upload.DeleteFile("uploads/user/paymentproof/", data.PaymentProof);
        //            }

        //            data.PaymentProof = newFileName;
        //        }
        //    }
        //    await _IMasterSubscriptions.Add(data);
        //    return RedirectToAction("Index", "UserRental");
        //}

        [HttpPost]
        public async Task<IActionResult> Add(MasterSubscriptionsModel data, IFormFile PaymentProof)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "AuthUser");

            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState)
                {
                    if (item.Value.Errors.Count > 0)
                    {
                        Console.WriteLine($"MODEL ERROR: {item.Key} - {item.Value.Errors[0].ErrorMessage}");
                    }
                }
            }

            var user = await _IMasterUser.Find(userId.Value);

            data.UserId = user.Id;
            data.EndDate = DateTime.Now.AddMonths(data.Time);
            data.LastPaymentDate = DateTime.Now;
            data.Price = 300000;
            data.Status = 1;

            if (PaymentProof != null && PaymentProof.Length > 0)
            {
                string? newFileName = await _upload.UploadFile(PaymentProof, "uploads/user/paymentproof/");

                if (!string.IsNullOrEmpty(newFileName))
                    data.PaymentProof = newFileName;
            }

            await _IMasterSubscriptions.Add(data);
            return RedirectToAction("Index", "UserRental");
        }
    }
}
