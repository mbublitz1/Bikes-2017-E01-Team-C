using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs.JobingPOCO
{
    public class CurrentJobServiceDetails
    {
        public int ServiceID { get; set; }
        public string Description { get; set; }
        public decimal Hours { get; set; }
        public string Coupon { get; set; }
        public int CouponID { get; set; }
        public string Comments { get; set; }
    }
}
