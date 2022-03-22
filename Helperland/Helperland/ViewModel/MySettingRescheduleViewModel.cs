using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class MySettingRescheduleViewModel
    {
        [RegularExpression(@"^null|$", ErrorMessage = "Enter Date")]
        [Required]
        public string rescheduled_date { get; set; }

        [RegularExpression(@"^null|$", ErrorMessage = "Enter Time")]
        [Required]
        public string rescheduled_time { get; set; }
    }
}
