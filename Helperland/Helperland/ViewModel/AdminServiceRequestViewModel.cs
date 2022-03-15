using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class AdminServiceRequestViewModel
    {
        public int ser_id { get; set; }
        public DateTime start_date { get; set; }
        public double? duration { get; set; }
        public string cust_name { get; set; }
        public string addressline1 { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public int? sp_id { get; set; }
        public string sp_name { get; set; }
        public decimal? sp_rating { get; set; }
        public decimal amount { get; set; }
        public int? status { get; set; }



    }
}
