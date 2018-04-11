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
    public class JobDetailsController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<CurrentJobServiceDetails> List_JobService(int jobID)
        {
            using (var context = new eBikesContext())
            {
                var results = from x in context.JobDetails
                              where x.JobID == jobID
                              select new CurrentJobServiceDetails
                              {
                                  ServiceID = x.JobDetailID,
                                  Description = x.Description,
                                  Hours = x.JobHours,
                                  Coupon = x.Coupon.CouponIDValue,
                                  Comments = x.Comments
                              };

                return results.ToList();
            }
        }
        
        public void Delete_Service(JobDetail item)
        {
  
                Delete_Service(item.JobDetailID);
     
        }
        public void Delete_Service(int JobDetailID)
        {
            using (var context = new eBikesContext())
            {
                var existing = context.JobDetails.Find();
                if (existing == null)
                {
                    throw new Exception("Job detail does not exist in database.");
                }
                if (existing.Completed == null)
                {
                    context.JobDetails.Remove(existing);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cannot delete  because service had started");
                }
                
            }
        }
        public void Add_Service(JobDetail item)
        {
            using (var context = new eBikesContext())
            {
                context.JobDetails.Add(item);
                context.SaveChanges();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<JobDetailStatus> List_JobDetailStatus(int JobID)
        {
            using (var context = new eBikesContext())
            {
                var results = from x in context.JobDetails
                              where x.JobID == JobID
                              select new JobDetailStatus
                              {
                                 JobDetailID = x.JobDetailID,
                                  Description = x.Description, 
                                   Status = x.Completed == true? "Done":
                              x.Completed == false? "Started":
                              x.Completed == null? "" : ""
                              
                                  
                              };

                return results.ToList();
            }
        }
    }
}
