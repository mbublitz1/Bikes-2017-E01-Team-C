using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eBike.System.BLL;
using eBike.System.BLL.Security;
using eBike.Data.Entities;
using eBike.Data.Entities.Security;
#endregion

public partial class Admin_Security_UserRoleAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if(!Request.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                if(!User.IsInRole(SecurityRoles.Admin))
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
            }
        }
    }
    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void RefreshAll(object sender, EventArgs e)
    {
        DataBind();
    }

    protected void UserListView_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        var addtoroles = new List<string>();
        var roles = e.Item.FindControl("RoleMemberships") as CheckBoxList;

        if(roles != null)
        {
            foreach (ListItem item in roles.Items)
            {
                if(item.Selected)
                {
                    addtoroles.Add(item.Value);
                }
            }
            e.Values["RoleMemberships"] = addtoroles;
        }
    }
}