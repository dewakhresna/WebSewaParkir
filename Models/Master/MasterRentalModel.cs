using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Master
{
    public class MasterRentalModel
    {
        public int Id { get; set; }
        public string? NoPolice { get; set; }
        public int IdKendaraan { get; set; }
        public int UserId { get; set; }

        [ForeignKey("IdKendaraan")]
        public MasterKendaraanModel? Kendaraan { get; set; }
        [ForeignKey("UserId")]
        public MasterUserModel? User { get; set; }
        public MasterSubscriptionsModel Payment { get; set; }
    }
}
