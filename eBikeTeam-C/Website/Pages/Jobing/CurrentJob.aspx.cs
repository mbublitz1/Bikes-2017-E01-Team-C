using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eBike.System.BLL.Jobing;
using eBike.System.BLL.Security;
using eBike.Data.Entities;
using eBike.Data.Entities.Security;
using eBike.Data.POCOs.JobingPOCO;
#endregion
public partial class Jobing_CurrentJob : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            UserFullName.Text = User.Identity.Name.ToString();
           
            string jid= Request.QueryString["jid"];
            string name = Request.QueryString["name"];
            string phone = Request.QueryString["phone"];
            if (!string.IsNullOrEmpty(jid))
            {

                JobID.Text = jid;
                CustomerName.Text = name;
                ContactNumber.Text = phone;

            }

        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void PresetButton_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(()=>
        {
            if (PresetDDL.SelectedIndex >= 0)
            {
                int selectedpreset = PresetDDL.SelectedIndex;
                StandardJobController sysmgr = new StandardJobController();
                List<StandardJob> standardjob = sysmgr.Job_GetByID(int.Parse(PresetDDL.SelectedValue));
                Description.Text = standardjob[selectedpreset].Description;
                Hours.Text = standardjob[selectedpreset].StandardHours.ToString();
            }
        });
        
    }

    protected void AddServiceButton_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (string.IsNullOrEmpty(CustomerName.Text) || string.IsNullOrEmpty(ContactNumber.Text))
            {
                MessageUserControl.ShowInfo("Empty Required Field", "Missing Customer Name or Contact Number.");
            }
            else
            {

                string customer = CustomerName.Text;
                string contactinfo = ContactNumber.Text;
                string comments = Comments.Text;

                if (JobID == null)
                {
                    MessageUserControl.TryRun(() =>
                {

                    JobController sysmgr = new JobController();
                    UserManager usrmgr = new UserManager();

                    ApplicationUser currentuser = (ApplicationUser) usrmgr.Users
                    .Where(u => u.EmployeeID != null)
                    .Select(x => x.UserName == User.Identity.Name.ToString());

                    List<Customer> exists = new List<Customer>();
                    Customer currentcustomer = exists.Find(c => c.ContactPhone == contactinfo);
                   
                    
                    Job newjob = new Job();
                    newjob.JobDateIn = DateTime.Today;
                    newjob.Customer.ContactPhone = ContactNumber.Text;
                    newjob.EmployeeID = currentuser.EmployeeID.Value;
                    newjob.Customer.CustomerID = currentcustomer.CustomerID;
                    newjob.VehicleIdentification = "Bike";
                    newjob.ShopRate = decimal.Parse("50.00");
                    newjob.StatusCode = "O";


                    CurrentJobServiceDetails newServ = new CurrentJobServiceDetails();
                    
                    
                        newServ.Comments = Comments.Text;
                        if (int.Parse(CouponDDL.SelectedValue) != 0)
                        {
                            newServ.CouponID = int.Parse(CouponDDL.SelectedValue);
                        }
                        newServ.Description = Description.Text;
                        newServ.Hours = decimal.Parse(Hours.Text);
                        
                    


                    sysmgr.Add_Job(newjob, newServ);
                    





                });

                }
                else
                {
                    MessageUserControl.TryRun(() => {

                        JobDetailsController sysmgr = new JobDetailsController();
                        JobDetail newjobdetail = new JobDetail();
                        newjobdetail.Comments = comments;
                        if (int.Parse(CouponDDL.SelectedValue) != 0)
                        {
                            newjobdetail.CouponID = int.Parse(CouponDDL.SelectedValue);
                        }
                        else
                        {
                            newjobdetail.CouponID = null;
                        }
                        newjobdetail.Description = Description.Text;
                        newjobdetail.JobID = int.Parse
                         (JobID.Text);
                        
                        newjobdetail.JobHours = decimal.Parse(Hours.Text);
                        sysmgr.Add_Service(newjobdetail);

                    });
                   

                }
            }
        }
    }
    protected void ServiceList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveService")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow rowselected = ServiceList.Rows[index];
            int jobserviceID = 0;
            JobDetail selectedJob = new JobDetail();
            
            if (selectedJob.Completed == null)
            {
                if (int.TryParse((rowselected.FindControl("ServiceID") as Label).Text, out jobserviceID))
                {
                    selectedJob.JobDetailID = jobserviceID;


                    MessageUserControl.TryRun(() =>
                    {
                        JobDetailsController sysmgr = new JobDetailsController();

                        


                        sysmgr.Delete_Service(jobserviceID);
                    }, "Update Job Detals", "Job detail as been successfully removed from database");

                }
                else
                {

                    MessageUserControl.ShowInfo("Invalid Data", "Cannot find job detail ID");
                }
            }
            else
            {
                MessageUserControl.ShowInfo("Action Failed", "Cannot remove service because it has started");
            }
        }
     
           
        
    }

    protected void StartJob_Click(object sender, EventArgs e)
    {
        string jobid = JobID.Text;
        string customer = CustomerName.Text;
        string contact = ContactNumber.Text;
        Response.Redirect(string.Format("CurrentServiceDetails.aspx?jid={0}&customer={1}&phone={2}", jobid, customer, contact),false);

    }
}