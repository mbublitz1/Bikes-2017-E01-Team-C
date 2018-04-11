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
    public class JobController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<CurrentJobs> List_CurrentJobs()
        {
            using (var context = new eBikesContext())
            {
                var results =
from x in context.Jobs
where x.JobDateOut==null
orderby x.JobDateIn
select new CurrentJobs
{
    JobID = x.JobID,
    In = x.JobDateIn,
    Started = x.JobDateStarted,
    Done = x.JobDateDone,
    Customer = x.Customer.LastName + ", " + x.Customer.FirstName,
    ContactNumber = x.Customer.ContactPhone
};
                return results.ToList();
            }
        }
          
        public void Add_Job(Job item, CurrentJobServiceDetails currentJobDetails)
        {
            using (var context = new eBikesContext())
            {
                context.Jobs.Add(item);
                JobDetail newjobDetail = new JobDetail();
                newjobDetail.CouponID = currentJobDetails.CouponID;
                newjobDetail.Comments = currentJobDetails.Comments;
                newjobDetail.Description = currentJobDetails.Description;
                newjobDetail.JobHours = currentJobDetails.Hours;

                item.JobDetails.Add(newjobDetail); 
            }
        }

    }
}
