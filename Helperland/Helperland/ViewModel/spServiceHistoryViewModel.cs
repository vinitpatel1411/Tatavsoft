using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class spServiceHistoryViewModel
    {
        public int service_id { get; set; }
        public string cust_name { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string addressline1 { get; set; }
        public DateTime service_datetime { get; set; }
        public double? duration { get; set; }
    }
}
