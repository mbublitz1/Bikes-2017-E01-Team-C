using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eBike.Data.POCOs;
using eBike.Data.Entities;
using eBike.System.DAL;
using System.ComponentModel;
#endregion

namespace eBike.System.BLL.Sales
{
    [DataObject]
    public class PartsController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<ProductCatalogItemPOCO> List_PartsByCategory(int catId, String username)
        {
            using (var context = new eBikesContext())
            {
                List<ProductCatalogItemPOCO> results = new List<ProductCatalogItemPOCO>();

                if (catId == 0)
                {
                    results = (from x in context.Parts
                                   orderby x.Description
                                   where x.Discontinued == false
                                   select new ProductCatalogItemPOCO
                                   {
                                       PartId = x.PartID,
                                       PartName = x.Description,
                                       UnitPrice = x.SellingPrice,
                                       QtyInStock = x.QuantityOnHand,
                                       CartQty = (from y in x.ShoppingCartItems
                                                  where y.ShoppingCart.OnlineCustomer.UserName.Equals(username)
                                                  select y.Quantity).FirstOrDefault()                                               
                                   }).ToList();
                }
                else
                {
                    results = (from x in context.Parts
                                   orderby x.Description
                                   where x.CategoryID == catId && x.Discontinued == false
                                   select new ProductCatalogItemPOCO
                                   {
                                       PartId = x.PartID,
                                       PartName = x.Description,
                                       UnitPrice = x.SellingPrice,
                                       QtyInStock = x.QuantityOnHand,
                                       CartQty = (from y in x.ShoppingCartItems
                                                  where y.ShoppingCart.OnlineCustomer.UserName.Equals(username)
                                                  select y.Quantity).FirstOrDefault()
                                   }).ToList();
                }

                return results;
            }
        }
    }
}
