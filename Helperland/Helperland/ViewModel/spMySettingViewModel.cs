using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class spMySettingViewModel
    {
        
        public List<NewServiceRequestViewModel> newServices { get; set; }
        public List<UpcomingServiceViewModel> upcomingServices { set; get; }
        public List<spServiceHistoryViewModel> spServiceHistories { get; set; }
        public List<spMyratingViewModel> spMyratings { get; set; }
        public List<spBlockCustomersViewModel> spBlockCustomers { get; set; }
        public int hidden_nsr_ser_id { get; set; }
        public int hidden_complete_ser_id { get; set; }
        public int hidden_cancel_ser_id { get; set; }
        public int customer_id { get; set; }
        //--------------------for sp detail screen----------------------
        public bool is_active { get; set; }
        public string Firsname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public int? nationalityid { get; set; }
        public int dob_day { get; set; }
        public int dob_month { get; set; }
        public int dob_year { get; set; }
        public int? gender { get; set; }
        public string avatar { get; set; }

        public string street { get; set; }
        public int house { get; set; }
        public string postal { get;set; }
        public string city { get; set; }
        public string pwd { get; set; }
        public int hidden_avatar { get; set; }
        public string new_pwd { get; set; }
    }
}
