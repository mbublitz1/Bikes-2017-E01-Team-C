using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region
using eBike.Data.POCOs;
using eBike.System.BLL.Sales;
#endregion

public partial class Pages_Sales : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Request.IsAuthenticated) {      
                string currentUser = User.Identity.Name;
                OnlineCustomerController custmgr = new OnlineCustomerController();
                custmgr.Add_New_OnlineCustomer(currentUser);
                DisplayCurrentCartQty();
                CurrentLoggedInUser.Text = User.Identity.Name;

            if (!Page.IsPostBack) { 
                var loggedInGridview = (GridView)ProductsLoggedIn.FindControl("ProductListByCatLoged");
                loggedInGridview.DataBind();
            }
        }

      
    }


    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }



    protected void CategoryFilter_Click(object sender, EventArgs e)
    {
        LinkButton linkBtn = (LinkButton)sender;
        CurrentCategoryId.Text = linkBtn.CommandArgument;
        string catDescription = (linkBtn.Parent.FindControl("DescriptionLabel") as Label).Text;
        catFilterType.Text = catDescription;
        if (Request.IsAuthenticated)
        {
            var loggedInGridview = (GridView)ProductsLoggedIn.FindControl("ProductListByCatLoged");
            loggedInGridview.DataBind();
        }
        DisplayCurrentCartQty();
    }

    protected void AddToCartBtn_Click(object sender, EventArgs e)
    {
        LinkButton linkBtn = (LinkButton)sender;
        string username = User.Identity.Name;
        GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
        int productid = int.Parse(linkBtn.CommandArgument.ToString());
        int quantity = int.Parse((grdrow.Cells[2].FindControl("AddQtyValue") as TextBox).Text);

        MessageUserControl.TryRun(() =>
        {
            ShoppingCartItemController sysmgr = new ShoppingCartItemController();
            sysmgr.Add_Product_To_ShoppingCart(username, productid, quantity);
            var loggedInGridview = (GridView)ProductsLoggedIn.FindControl("ProductListByCatLoged");
            loggedInGridview.DataBind();
            DisplayCurrentCartQty();

        }, "Added Product", "Product added to shopping cart!");
    }

    protected void DisplayCurrentCartQty()
    {
        string username = User.Identity.Name.ToString();
        ShoppingCartController sysmgr = new ShoppingCartController();
        string currentCount = sysmgr.ShowQtyCartAmount(username);
        CurretyItemsQtyCart.Text = currentCount;
    }

}
