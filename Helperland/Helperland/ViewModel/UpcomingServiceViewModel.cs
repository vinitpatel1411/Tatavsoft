using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class UpcomingServiceViewModel
    {
        public int cust_id { get; set; }
        public int service_id { get; set; }
        public DateTime service_date { get; set; }
        public double? duration { get; set; }
        public decimal service_amount { get; set; }
        public int extra_service { get; set; }
        public string service_address { get; set; }
        
        
        public string comment { get; set; }
        public bool has_pet { get; set; }
        public string cust_first_name { get; set; }
        public string cust_last_name { get; set; }
        public string pincode { get; set; }
        public decimal distance { get; set; }
        public string city { get; set; }
    }
}
