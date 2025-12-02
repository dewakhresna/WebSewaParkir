using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Master;

namespace Models.Master
{
    public class MasterSubscriptionsModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CarRentalId { get; set; }
        public int ParkirSlotId { get; set; }
        public int Time { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public int Price { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentProof { get; set; }
        public int Status { get; set; }
        public MasterRentalModel Car { get; set; }
        public MasterParkirSlotModel ParkirSlot { get; set; }
        public string StatusText
        {
            get
            {
                return Status switch
                {
                    1 => "Pembayaran Sedang Dalam Proses",
                    2 => "Pembayaran Berhasil",
                    0 => "Pembayaran Gagal",
                    _ => "Status Tidak Dikenal"
                };
            }
        }
        public string StatusInfo
        {
            get
            {
                return Status switch
                {
                    1 => "badge bg-info",
                    2 => "badge bg-success",
                    0 => "badge bg-danger",
                    _ => "badge bg-dark"
                };
            }
        }
        public string PriceFormatted
        {
            get
            {
                return string.Format(new System.Globalization.CultureInfo("id-ID"), "{0:C0}", Price)
                       .Replace("Rp", "Rp ");
            }
        }
    }
}
