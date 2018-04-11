using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Jobing_CurrentServiceDetails : System.Web.UI.Page
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

            string jid = Request.QueryString["jid"];
            string name = Request.QueryString["name"];
            string phone = Request.QueryString["phone"];
            if (!string.IsNullOrEmpty(jid))
            {

                JobNumber.Text = jid;
                CustomerName.Text = name;
                ContactNumber.Text = phone;

            }

        }
    }
    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }
    protected void ServiceParts_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    

    protected void StartServiceButton_Click(object sender, EventArgs e)
    {
        
    }

    protected void ServiceStatus_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow rowselected = ServiceStatus.Rows[index];
            
            int jobdetailId = int.Parse(rowselected.Cells[0].Text);
            

        }
        if (e.CommandName == "Done")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow rowselected = ServiceStatus.Rows[index];

            
        }
        if (e.CommandName == "Remove")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow rowselected = ServiceStatus.Rows[index];
        }
    }
}