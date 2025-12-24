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
        public string Occupied
        {
            get
            {
                return IsOccupied switch
                {
                    true => "Available",
                    false => "Not Available",
                };
            }
        }
        public string Set
        {
            get
            {
                return Status switch
                {
                    1 => "Active",
                    0 => "Inactive",
                };
            }
        }
    }
}
