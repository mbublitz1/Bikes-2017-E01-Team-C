using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eBike.Data.POCOs;
#endregion

namespace eBike.Data.DTOs
{
    public class ShoppingCartCheckoutDTO
    {
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }

        public List<ShoppingCartLineItemPOCO> ShoppingCartItems { get; set; }
     }
}
