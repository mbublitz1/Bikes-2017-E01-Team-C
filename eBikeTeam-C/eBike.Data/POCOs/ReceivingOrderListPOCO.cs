using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class ReceivingOrderListPOCO
    {
        public int PurchaseOrderId { get; set; }
        public int? PurchaseOrderNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public string VendorName { get; set; }
        public string VendorPhone { get; set; }
    }
}
