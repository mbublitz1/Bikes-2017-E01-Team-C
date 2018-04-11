using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using eBike.Data.Entities;
using eBike.Data.POCOs;
using eBike.Data.DTOs;
using System.ComponentModel;
using eBike.System.DAL;
#endregion

namespace eBike.System.BLL.Sales
{
    [DataObject]
    public class CouponController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Coupon> GetListOfAvailableCoupons(DateTime currentDate)
        {
            using (var context = new eBikesContext())
            {
                var results = (from x in context.Coupons
                               where x.EndDate >= currentDate && x.SalesOrService == 1
                               select x).ToList();
                return results;
            }
        }


        public decimal GetCouponAmount(int couponId)
        {
            using (var context = new eBikesContext())
            {
                int cpnAmount = (from x in context.Coupons
                                 where x.CouponID == couponId
                                 select x.CouponDiscount).FirstOrDefault();
                decimal cpnPercent = cpnAmount / Convert.ToDecimal(100.00);
                return cpnPercent;
            }

        }
    }
}

