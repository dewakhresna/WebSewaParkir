using KandangMobil.Helpers;
using KandangMobil.Interfaces;
using Microsoft.AspNetCore.Hosting;
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
            var carRental = await _IMasterRental.Find(Id);

            if (carRental == null)
            {
                return NotFound(); 
            }

            var kendaraan = await _IMasterKendaraan.Find(carRental.IdKendaraan);

            var model = new MasterSubscriptionsModel
            {
                CarRentalId = carRental.Id,
                Car = carRental,         
                Kendaraan = kendaraan,   
                Price = 0                 
            };

            ViewBag.Price = await _IMasterPrice.Get();

            return View(model);
        }

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

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var subscription = await _IMasterSubscriptions.Find(Id);

            if (subscription == null)
            {
                return NotFound();
            }

            // Pastikan data relasi (Car & Kendaraan) ter-load
            // Jika repository .Find() Anda belum melakukan Include, Anda mungkin perlu mengambil manual:
            subscription.Car = await _IMasterRental.Find(subscription.CarRentalId);
            subscription.Kendaraan = await _IMasterKendaraan.Find(subscription.Car.IdKendaraan);

            // Namun idealnya repository .Find() untuk Subscription sudah meng-include Car & Kendaraan.

            return View(subscription);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePayment(int id, IFormFile? NewPaymentProof)
        {
            // 1. Ambil data transaksi lama dari database
            var existingData = await _IMasterSubscriptions.Find(id);

            if (existingData == null)
            {
                return NotFound();
            }

            // 2. Cek apakah user mengupload file baru
            if (NewPaymentProof != null && NewPaymentProof.Length > 0)
            {
                // Tentukan folder penyimpanan (harus sama persis string-nya)
                string folderPath = "uploads/user/paymentproof/";

                // A. HAPUS FILE LAMA MENGGUNAKAN HELPER
                // Cek dulu apakah di database ada nama filenya
                if (!string.IsNullOrEmpty(existingData.PaymentProof))
                {
                    // Panggil fungsi DeleteFile dari Helper Anda
                    // Parameter: (Folder, NamaFile)
                    _upload.DeleteFile(folderPath, existingData.PaymentProof);
                }

                // B. UPLOAD FILE BARU MENGGUNAKAN HELPER
                // Parameter: (FileObject, Folder)
                string? newFileName = await _upload.UploadFile(NewPaymentProof, folderPath);

                // C. Update nama file di object model jika upload sukses
                if (!string.IsNullOrEmpty(newFileName))
                {
                    existingData.PaymentProof = newFileName;
                }
            }

            // 3. Update Status Transaksi
            // Kembalikan status ke 1 (Sedang Diproses) agar admin mengecek ulang
            existingData.Status = 1;

            // Update tanggal pembayaran terakhir
            existingData.LastPaymentDate = DateTime.Now;

            // 4. Simpan Perubahan ke Database
            await _IMasterSubscriptions.Update(existingData);

            // 5. Kembali ke halaman list
            return RedirectToAction("Index", "UserRental");
        }
    }
}
