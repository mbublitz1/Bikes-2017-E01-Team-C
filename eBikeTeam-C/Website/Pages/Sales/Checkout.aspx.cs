using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using eBike.System.BLL.Sales;
using eBike.Data.POCOs;
using eBike.Data.Entities;
#endregion

public partial class Pages_Sales_Checkout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Request.IsAuthenticated)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        { 
            currentUser.Text = User.Identity.Name.ToString();
            FetchShoppingCartTotal();
            if (!Page.IsPostBack)
            {
                FetchCouponList();
            }
        }
    }

    protected void FetchShoppingCartTotal()
    {
        string username = User.Identity.Name.ToString();
        ShoppingCartController sysmgr = new ShoppingCartController();
        decimal total = sysmgr.GetShoppingCartTotal(username);
        TotalValue.Text = string.Format("{0:C2}", total);
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void FetchCouponList()
    {
        MessageUserControl.TryRun(() =>
        {
            DateTime currentDate = DateTime.Now;
            CouponController sysmgr = new CouponController();
            List<Coupon> currentCopns = sysmgr.GetListOfAvailableCoupons(currentDate);
            PromoCodeDropDownList.DataSource = currentCopns;
            PromoCodeDropDownList.DataValueField = "CouponID";
            PromoCodeDropDownList.DataTextField = "CouponIDValue";
            PromoCodeDropDownList.DataBind();
        });
    }

    protected void PromoCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Label saleDiscount = new Label();
        Label saleFinalTotal = new Label();
        string username = User.Identity.Name.ToString();
        ShoppingCartController totalmgr = new ShoppingCartController();
        decimal total = totalmgr.GetShoppingCartTotal(username);

        if (PromoCodeDropDownList.SelectedValue == "0")
        {
            DiscountPercentValue.Text = "0.00 %";
            DiscountValue.Text = "$0.00";

            
            foreach (RepeaterItem item in CheckoutRepeaterView.Items)
            {
                saleDiscount = item.FindControl("SaleDiscountValue") as Label;
                saleDiscount.Text = "$0.00";
                saleFinalTotal = item.FindControl("SaleFinalTotal") as Label;
                saleFinalTotal.Text = string.Format("{0:C2}", total);
            }
        }
        else
        { 
            CouponController sysmgr = new CouponController();
            decimal discountPercent = sysmgr.GetCouponAmount(int.Parse(PromoCodeDropDownList.SelectedValue));
            DiscountPercentValue.Text = string.Format("{0:P2}", discountPercent);
            DiscountValue.Text = string.Format("{0:C2}", (total * discountPercent));

            foreach (RepeaterItem item in CheckoutRepeaterView.Items)
            {
                saleDiscount = item.FindControl("SaleDiscountValue") as Label;
                saleDiscount.Text = string.Format("{0:C2}", (total * discountPercent));          
                saleFinalTotal = item.FindControl("SaleFinalTotal") as Label;
                saleFinalTotal.Text = string.Format("{0:C2}", (total * (1 - discountPercent)));
            }
        }
    }

    protected void PaymentTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selected = PaymentTypeList.SelectedValue;
        Label paymenttype = new Label();
        Image paytypeimgage = new Image();
        if (selected == "c")
        {
            foreach (RepeaterItem item in CheckoutRepeaterView.Items)
            {
                paymenttype = item.FindControl("SalePayType") as Label;
                paymenttype.Text = "Credit";
                paymenttype.CssClass = "checkout-payment-type-value";
                paytypeimgage = item.FindControl("SalePatImg") as Image;
                paytypeimgage.ImageUrl = "/img/paymnet-credit.png";
            }
        } else if (selected == "d")
        {
            foreach (RepeaterItem item in CheckoutRepeaterView.Items)
            {
                paymenttype = item.FindControl("SalePayType") as Label;
                paymenttype.Text = "Debit";
                paymenttype.CssClass = "checkout-payment-type-value-d";
                paytypeimgage = item.FindControl("SalePatImg") as Image;
                paytypeimgage.ImageUrl = "/img/paymnet-debit.png";
            }
        }
    }

    protected void CreateSaleBtn_Click(object sender, EventArgs e)
    {
        string username = User.Identity.Name;

        ShoppingCartController totalmgr = new ShoppingCartController();
        decimal total = totalmgr.GetShoppingCartTotal(username);
        CouponController sysmgr = new CouponController();
        decimal discountPercent = sysmgr.GetCouponAmount(int.Parse(PromoCodeDropDownList.SelectedValue));

        decimal subtotal = total * (1 - discountPercent);
        string paymentType = PaymentTypeList.SelectedValue.ToUpper();

        int couponId = int.Parse(PromoCodeDropDownList.SelectedValue);

        MessageUserControl.TryRun(() =>
        {
            SalesController salemgr = new SalesController();
            int sid = salemgr.CreatSaleFromCart(username, subtotal, couponId, paymentType);
            var url = String.Format("~/Pages/Sales/SaleConfirmation.aspx?sid={0}", sid);
            Response.Redirect(url);
        });
    }
}