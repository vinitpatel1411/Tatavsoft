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

        public AdminServiceAddressViewModel adminServiceAddress { get; set; }
        public int hidden_ser_id { get; set; }
        public int hidden_u_id { get; set; }
    }
}
