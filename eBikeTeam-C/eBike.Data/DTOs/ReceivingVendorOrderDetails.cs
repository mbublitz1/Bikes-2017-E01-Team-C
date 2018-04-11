using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBike.Data.POCOs;

namespace eBike.Data.DTOs
{
    public class ReceivingVendorOrderDetails
    {
        public int? PONumber { get; set; }
        public string VendorName { get; set; }
        public string Phone { get; set; }

        public List<ReceivingOrderDetailsPOCO> ReceivingOrderDetails { get; set; }

    }
}
