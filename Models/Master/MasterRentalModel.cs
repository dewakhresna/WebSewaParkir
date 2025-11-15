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
        public string? CustomerName { get; set; }
        public string? NoPolice { get; set; }
        public string? CarName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
