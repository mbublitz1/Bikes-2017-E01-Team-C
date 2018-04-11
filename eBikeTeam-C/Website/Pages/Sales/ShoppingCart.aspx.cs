using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eBike.System.BLL;
using eBike.System.BLL.Sales;
#endregion

public partial class Pages_Sales_ShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            string username = User.Identity.Name.ToString();
            CurrentUser.Text = username;
            FetchShoppingCartTotal();
            DisplayCurrentCartQty();        
            if (!CheckForItemsInCart())
            {
                checkoutBtn.HRef = "~/Pages/Sales/PartsCatalog.aspx";
            }
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void FetchShoppingCartTotal()
    {
        string username = User.Identity.Name.ToString();
        ShoppingCartController sysmgr = new ShoppingCartController();
        decimal total = sysmgr.GetShoppingCartTotal(username);
        CurrentCartTotal.Text = string.Format("{0:C2}", total);
    }

    protected void DisplayCurrentCartQty()
    {
        string username = User.Identity.Name.ToString();
        ShoppingCartController sysmgr = new ShoppingCartController();
        string currentCount = sysmgr.ShowQtyCartAmount(username);
        CurretyItemsQtyCart.Text = currentCount;
    }


    protected void DeleteItemBtn_Click(object sender, EventArgs e)
    {
        LinkButton cmdBtn = (LinkButton)sender;
        int shoppingcartitemid = int.Parse(cmdBtn.CommandArgument);
        MessageUserControl.TryRun(() =>
        {
            ShoppingCartController sysmgr = new ShoppingCartController();
            sysmgr.DeleteItemFromCart(User.Identity.Name, shoppingcartitemid);
            ShoppingCartItemListView.DataBind();
            FetchShoppingCartTotal();
            DisplayCurrentCartQty();
            if (!CheckForItemsInCart())
            {
                checkoutBtn.HRef = "~/Pages/Sales/PartsCatalog.aspx";
            }
        }, "Removal Successful", "Part was successfully removed form your shopping cart");
    }

    protected void UpdateQty_Click(object sender, EventArgs e)
    {
        LinkButton cmdBtn = (LinkButton)sender;
        string direction = cmdBtn.CommandArgument.ToString();
        int partid = int.Parse((cmdBtn.Parent.FindControl("ProductIdLabel") as Label).Text);
        MessageUserControl.TryRun(() =>
        {
            ShoppingCartController sysmgr = new ShoppingCartController();
            sysmgr.ChangeQtyOfPart(User.Identity.Name, partid, direction);
            ShoppingCartItemListView.DataBind();
            FetchShoppingCartTotal();
            DisplayCurrentCartQty();
            if (!CheckForItemsInCart())
            {
                checkoutBtn.HRef = "~/Pages/Sales/PartsCatalog.aspx";
            }
        }, "Quantity Updated", "Part Quantity has been updated");
    }


    protected bool CheckForItemsInCart()
    {
        ShoppingCartItemController sysmgr = new ShoppingCartItemController();
        bool check = sysmgr.CheckForShoppingCartItems(User.Identity.Name);
        return check;
    }
}