using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eBike.System.BLL;
using eBike.Data.DTOs;
using eBike.Data.Entities;
using eBike.Data.POCOs;

public partial class Pages_Receiving : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Request.IsAuthenticated)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void ViewOrder_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            GridViewRow grdRow = (GridViewRow)((LinkButton)sender).NamingContainer;
            int purchaseOrderId = int.Parse(((Label)grdRow.FindControl("purchaseOrderID")).Text);

            ReceivingOrderController sysmgr = new ReceivingOrderController();
            ReceivingVendorOrderDetails orderDetail = sysmgr.Get_OrderDetails(purchaseOrderId);

            PONumber.Text = orderDetail.PONumber.ToString();
            VendorName.Text = orderDetail.VendorName;
            Contact.Text = orderDetail.Phone;

            OrderDetailGrid.DataSource = orderDetail.ReceivingOrderDetails;
            OrderDetailGrid.DataBind();

            enableButtons();
        });
    }

    protected void forceCloserBtn_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (string.IsNullOrEmpty(reasonClose.Text))
            {
                MessageUserControl.ShowInfo("Missing Information", "Please ensure the reason the purchase order is being closed has been entered.");
            }
            else
            {
                MessageUserControl.TryRun(() =>
                {
                    ReceivingOrderController sysmgr = new ReceivingOrderController();
                    PurchaseOrder newPO = new PurchaseOrder();
                    newPO.PurchaseOrderNumber = int.Parse(PONumber.Text);
                    newPO.Closed = true;
                    newPO.Notes = reasonClose.Text;

                    List<ReceivingOrderDetailsPOCO> orderDetails = new List<ReceivingOrderDetailsPOCO>();

                    foreach (GridViewRow row in OrderDetailGrid.Rows)
                    {
                        ReceivingOrderDetailsPOCO newOrderDetail = new ReceivingOrderDetailsPOCO();
                        newOrderDetail.PurchaseOrderId = int.Parse(((Label)row.FindControl("PurchaseOrderId")).Text);
                        newOrderDetail.PartId = int.Parse(((Label)row.FindControl("PartId")).Text);
                        newOrderDetail.QtyOutstanding = int.Parse(((Label)row.FindControl("QtyOutstanding")).Text);

                        orderDetails.Add(newOrderDetail);
                    }

                    sysmgr.Update_ClosePO(newPO, orderDetails);

                    refreshForm();

                }, "Force Closer", "Purchase order has been successfully close");
            }
        }
    }

    protected void receiveBtn_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            MessageUserControl.TryRun(() =>
            {
                ReceivingOrderController sysmgr = new ReceivingOrderController();
                List<ReceiveNewOrders> recNewOrders = new List<ReceiveNewOrders>();
                int poId = 0;
                int poDetailId = 0;
                int qty = 0;
                int poNumber = int.Parse(PONumber.Text);

                foreach (GridViewRow row in OrderDetailGrid.Rows)
                {
                    poId = int.Parse(((Label)row.FindControl("PurchaseOrderId")).Text);
                    poDetailId = int.Parse(((Label)row.FindControl("PurchaseOrderDetailId")).Text);
                    string qtyReceived = ((TextBox)row.FindControl("receiving")).Text;
                    string qtyReturning = ((TextBox)row.FindControl("returning")).Text;
                    string reason = ((TextBox)row.FindControl("reason")).Text;
                    string outstanding = ((Label)row.FindControl("qtyOutstanding")).Text;
                    string partId = ((Label)row.FindControl("partId")).Text;
                    string partDescription = ((Label)row.FindControl("description")).Text;

                    if (string.IsNullOrEmpty(qtyReturning) && !string.IsNullOrEmpty(reason))
                    {
                        MessageUserControl.ShowInfo("Entry Error", "A reason can not be provided if no return is being made.");
                    }
                    else
                    {
                        ReceiveNewOrders recOrderDetail = new ReceiveNewOrders();
                        recOrderDetail.PurchaseOrderDetailId = poDetailId;
                        if (!string.IsNullOrEmpty(qtyReceived)) recOrderDetail.QtyReceived = int.Parse(qtyReceived);
                        if (!string.IsNullOrEmpty(qtyReturning)) recOrderDetail.QtyReturned = int.Parse(qtyReturning);
                        recOrderDetail.Notes = reason;
                        if (!string.IsNullOrEmpty(partId)) recOrderDetail.PartId = int.Parse(partId);
                        recOrderDetail.PartDescription = partDescription;
                        if (!string.IsNullOrEmpty(outstanding)) recOrderDetail.Outstanding = int.Parse(outstanding);
                        recNewOrders.Add(recOrderDetail);
                    }

                }
                sysmgr.Add_ReceivedOrders(poId, poNumber, recNewOrders);

                refreshForm();

                disableButtons();

            }, "Order", "New items have been added successfully");
        }
    }

    private void enableButtons()
    {
        receiveBtn.Enabled = true;
        forceCloserBtn.Enabled = true;
        reasonClose.Enabled = true;
    }

    private void disableButtons()
    {
        receiveBtn.Enabled = false;
        forceCloserBtn.Enabled = false;
        reasonClose.Enabled = false;
    }

    private void refreshForm()
    {
        OrderDetailGrid.DataSource = null;
        OrderDetailGrid.DataBind();
        OrderListGrid.DataBind();
        UnorderedPartsList.DataBind();
        reasonClose.Text = string.Empty;
        PONumber.Text = string.Empty;
        VendorName.Text = string.Empty;
        Contact.Text = string.Empty;
    }
}