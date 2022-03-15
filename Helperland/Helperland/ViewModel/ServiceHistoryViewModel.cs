using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ServiceHistoryViewModel
    {
        public int service_id { get; set; }
        public int? status { get; set; }
        public DateTime service_date { get; set; }
        public decimal service_amount { get; set; }
        public double? duration { get; set; }
        public decimal? sp_rating { get; set; }
        public string sp_name { get; set; }
    }
}
