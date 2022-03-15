using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class DashboardServiceViewModel
    {
        public int service_id { get; set; }
        public DateTime service_date { get; set; }
        public double? duration { get; set; }
        public decimal service_amount { get; set; }
        public int extra_service { get; set; }
        public string service_address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string comment { get; set; }
        public bool has_pet { get; set; }
        public decimal? sp_rating { get; set; }
        public string sp_name { get; set; }
    }
}
