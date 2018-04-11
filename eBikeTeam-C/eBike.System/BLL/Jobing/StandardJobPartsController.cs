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
   public class StandardJobPartsController
   {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<JobDetailParts> List_StandardJobparts(int standardJobID)
        {
            using (var context = new eBikesContext())
            {
                var results = from x in context.StandardJobParts
                              where x.StandardJobID == standardJobID
                              select new JobDetailParts
                              {
                                  Description = x.Part.Description,
                                  PartID = x.PartID,
                                  Qty = x.Quantity

                              };
                return results.ToList();

            }
        }
   }
}
