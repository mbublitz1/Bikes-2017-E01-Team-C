using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#region Additional NameSpaces
using eBike.Data.Entities.Security;
using eBike.Data.Entities;
using eBike.System.BLL.Jobing;
using eBike.System.BLL.Security;
using eBike.System.BLL;
#endregion
public partial class Jobing_CurrentJobList : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {



            UserFullName.Text = User.Identity.Name;
        }

    }
    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }
    protected void CurrentJobsList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
 
        if (e.CommandName == "ViewCurrentJob")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow rowSelected = CurrentJobList.Rows[index];

            int jobID = int.Parse(rowSelected.Cells[0].Text);
            string customerName = rowSelected.Cells[4].Text;
            string contactNumber = rowSelected.Cells[5].Text;
            string startDate = rowSelected.Cells[2].Text;
            Response.Redirect(string.Format("CurrentJob.aspx?jid={0}&name={1}&phone={2}&startdate={3}", jobID, customerName, contactNumber, startDate),false);
           
        }
        
    }

    protected void NewJobButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("CurrentJob.aspx?");
    }
}