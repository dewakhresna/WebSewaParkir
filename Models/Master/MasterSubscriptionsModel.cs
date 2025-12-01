using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
