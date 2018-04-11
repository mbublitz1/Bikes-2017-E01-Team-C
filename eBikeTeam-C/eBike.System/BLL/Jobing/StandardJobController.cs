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
    public class StandardJobController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<StandardJob>List_Preset()
        {
            using (var context = new eBikesContext())
            {
                var results = from x in context.StandardJobs
                              select x;
                return results.ToList(); 
            }
        }
        public List<StandardJob> Job_GetByID(int standardJobID)
        {
            using (var context = new eBikesContext())
            {
                var results = from x in context.StandardJobs
                              where x.StandardJobID == standardJobID
                              select x;
                return results.ToList();
            }
        }
    }
}
