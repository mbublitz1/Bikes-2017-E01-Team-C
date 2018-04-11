using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class ProductCatalogItemPOCO
    {
        public int PartId { get; set; }
        public int CartQty { get; set; }
        public string PartName { get; set; }
        public decimal UnitPrice { get; set; }
        public int QtyInStock { get; set; }
    }
}
