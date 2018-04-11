using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBike.Data.POCOs
{
    public class ShoppingCartLineItemPOCO
    {
        public int ShoppingCartItemId{ get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public decimal ItemTotal { get; set; }
        public string Backorder { get; set; }
    }
}
