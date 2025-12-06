using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Master;

namespace Models.Master
{
    public class PaymentDetailViewModel
    {
        public MasterSubscriptionsModel Payment { get; set; }
        public MasterRentalModel Rental { get; set; }
        public MasterKendaraanModel Kendaraan { get; set; }
        public MasterUserModel User { get; set; }
    }
}
