using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class ReceivingOrderDetailsPOCO
    {
        public int PurchaseOrderId { get; set; }
        public int PurchaseOrderDetailId { get; set; }
        public int PartId { get; set; }
        public string PartDescription { get; set; }
        public int QtyOnOrder { get; set; }
        public int QtyOutstanding { get; set; }
    }
}
