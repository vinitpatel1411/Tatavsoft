using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class spMyratingViewModel
    {
        public int service_id { get; set; }
        public string cust_name { get; set; }
        public DateTime service_datetime { get; set; }
        public decimal? rating { get; set; }
        public string cust_feedback { get; set; }
        public double? duration { get; set; }
    }
}
