using Helperland.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class MySettingViewModel
    {
        public User user { get; set; }
        public MySetingDetailViewModel my { get; set; }
        public List<UserAddress> userAddresses { get; set; }
        public int dob_day { get; set; }
        public int dob_month { get; set; }
        public int dob_year { get; set; }
        public ForgotPasswordViewModel forgot { get; set; }
        public string pwd { get; set; }

        public MySettingAddressViewModel myAddress { get; set; }

        public int hidden_add_id { get; set; }
        public int delete_add_id { get; set; }


        // ---------------------------for dashboard --------------------------
        public List<DashboardServiceViewModel> futureRequests { get; set; }

        public MySettingRescheduleViewModel mySettingReschedule { get; set; }

        public int hidden_service_id { get; set; }
        public int hidden_delete_service { get; set; }
        //----------------------------for service history ----------------------------
        public List<ServiceHistoryViewModel> pastServices { get; set; }
        //----------------------------for ratings -------------------------------
        public decimal on_time_arrival { get; set; }
        public decimal freindly { get; set; }
        public decimal qualit_of_service { get; set; }
        public string feedback { get; set; }
        public int rate_ser_id { get; set; }
    }
}
