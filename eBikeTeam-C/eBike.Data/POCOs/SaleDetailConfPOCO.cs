using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class SaleDetailConfPOCO
    {
        public int SaleDetailID { get; set; }
        public int PartID { get; set; }
        public string PartName { get; set; }
        public int Qty { get; set; }
        public decimal SellingPrice { get; set; }
        public bool BackOrder { get; set; }
        public DateTime? shippedDate { get; set; }
    }
}
