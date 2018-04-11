using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#region Additional Namespaces
using eBike.Data.Entities;
using eBike.Data.POCOs.JobingPOCO;
using eBike.System.DAL;
using System.ComponentModel;
#endregion
namespace eBike.System.BLL.Jobing
{
    [DataObject]
   public class CouponController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Coupon> List_Coupon()
        {
            using (var context = new eBikesContext())
            {
                var results = from x in context.Coupons
                              where x.SalesOrService == 2
                              select x;

                return results.ToList();
            }
        }


    }
}
