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
using System.ComponentModel.DataAnnotations;
#endregion
namespace eBike.System.BLL
{
        [DataObject]
        public class VendorController
        {
            [DataObjectMethod(DataObjectMethodType.Select, false)]
            public List<VendorList> Get_Vendors()
            {
                using (var context = new eBikesContext())
                {
                    var results = from x in context.Vendors
                                  select new VendorList
                                  {
                                      VendorID = x.VendorID,
                                      VendorName = x.VendorName
                                  };

                    return results.ToList();
                }
            }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<VendorList> Get_VendorInformation(int vendorid)
        {
            using (var context = new eBikesContext())
            {
                var results = from x in context.Vendors
                              where x.VendorID == vendorid
                              select new VendorList
                              {
                                  VendorID = x.VendorID,
                                  VendorName = x.VendorName,
                                  address = x.Address,
                                  city = x.City,
                                  phone = x.Phone,
                                  postalcode = x.PostalCode,
                                  province = x.ProvinceID
                              };

                return results.ToList();
            }
        }
    }
}
