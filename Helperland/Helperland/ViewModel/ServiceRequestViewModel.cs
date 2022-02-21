using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.ViewModel
{
    public class ServiceRequestViewModel
    {
        public string servicestartdate { get; set; }
        public string servicestrarttime { get; set; }
        public float servicehours { get; set; }
        public float subtotal { get; set; }
        public float totalcost { get; set; }
        public string comments { get; set; }
        public bool haspets { get; set; }
        public bool extraSer1 { get; set; }
        public bool extraSer2 { get; set; }
        public bool extraSer3 { get; set; }
        public bool extraSer4 { get; set; }
        public bool extraSer5 { get; set; }
    }
}
