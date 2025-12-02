using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Master
{
    public class MasterPriceModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
    }
}
