using Helperland.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class AdminViewModel
    {
        public List<User> users { get; set; }
        public List<AdminServiceRequestViewModel> adminServiceRequests { get; set; }
        public string c_ser_date { get; set; }
        public string c_ser_time { get; set; }
        public string c_street { get; set; }
        public string c_hno { get; set; }
        public string c_postal { get; set; }
        public string c_city { get; set; }
        public int hidden_ser_id { get; set; }
        public int hidden_u_id { get; set; }
    }
}
