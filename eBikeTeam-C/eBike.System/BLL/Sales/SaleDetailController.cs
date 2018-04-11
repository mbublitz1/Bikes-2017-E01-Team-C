using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eBike.Data.Entities;
using eBike.Data.POCOs;
using eBike.System.DAL;
using System.ComponentModel;
using System.Data.Entity;
#endregion

namespace eBike.System.BLL.Sales
{
    [DataObject]
    public class SaleDetailController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SaleDetailConfPOCO> GetSaleItemDetails(int saleid)
        {
            using (var context = new eBikesContext())
            {
                var result = (from x in context.SaleDetails
                              where x.Sale.SaleID == saleid
                              select new SaleDetailConfPOCO
                              {
                                  SaleDetailID = x.SaleDetailID,
                                  PartID = x.PartID,
                                  PartName = x.Part.Description,
                                  Qty = x.Quantity,
                                  SellingPrice = x.SellingPrice,
                                  BackOrder = x.Backordered,
                                  shippedDate = x.ShippedDate
                              }).ToList();

                return result;
            }
        }
    }
}
