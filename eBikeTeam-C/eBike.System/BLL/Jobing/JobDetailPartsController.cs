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
{   [DataObject]
    public class JobDetailPartsController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<JobDetailParts> List_JobDetailParts(int jobdetailid)
        {
            using (var context = new eBikesContext())
            {
                var results = from x in context.JobDetailParts
                             where x.JobDetailID == jobdetailid
                             select new JobDetailParts
                             {

                                 PartID = x.PartID,
                                 Description = x.Part.Description,
                                 Qty = x.Quantity

                             };
                return results.ToList();
            }
        }
    }
}
