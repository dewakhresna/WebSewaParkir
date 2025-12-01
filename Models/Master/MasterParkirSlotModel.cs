using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Master
{
    public class MasterParkirSlotModel
    {
        public int Id { get; set; }
        public string SlotNumber { get; set; }
        public Boolean IsOccupied { get; set; }
        public int Status { get; set; }
    }
}
