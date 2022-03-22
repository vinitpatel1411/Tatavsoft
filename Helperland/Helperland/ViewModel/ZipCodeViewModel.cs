using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ZipCodeViewModel
    {
        [RegularExpression(@"^[1-9]{1}[0-9]{5}$",ErrorMessage ="enter valid zipcode")]
        [Required(ErrorMessage ="Enter zipcode")]
        public string zipcode { get; set; }
    }
}
