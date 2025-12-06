using System;
using System.Collections.Generic;
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
        public MasterKendaraanModel? Kendaraan { get; set; }
        public MasterUserModel? User { get; set; }
    }
}
