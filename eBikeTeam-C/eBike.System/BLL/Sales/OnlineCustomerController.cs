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
#endregion

namespace eBike.System.BLL.Sales
{
    public class OnlineCustomerController
    {
        public void Add_New_OnlineCustomer(string username)
        {
            using (var context = new eBikesContext())
            {
                var exsists = (from x in context.OnlineCustomer where x.UserName.Equals(username) select x).FirstOrDefault();

                if(exsists == null)
                {
                    Guid trackingCookie;
                    trackingCookie = Guid.NewGuid();
                    exsists = new OnlineCustomer();
                    exsists.UserName = username;
                    exsists.CreatedOn = DateTime.Now;
                    exsists.TrackingCookie = trackingCookie;
                    context.OnlineCustomer.Add(exsists);
                    context.SaveChanges();
                }
            }
        }
    }
}
