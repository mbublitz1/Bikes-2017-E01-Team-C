using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eBike.System.BLL;
using eBike.Data.POCOs;
using eBike.Data.Entities;

public partial class Pages_Purchasing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hide_everything();
    }


    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void SearchEvent_Click(object sender, EventArgs e)
    {
        if (VendorsDisplay.SelectedValue == "0")
        {
            MessageUserControl.ShowInfo("Please select a valid vendor ID");
        }
        else
        {
            show_everything();
            PurchaseOrderController Sysmgr = new PurchaseOrderController();

            List<PurchaseOrderList> newOrder = new List<PurchaseOrderList>();
            newOrder = Sysmgr.Get_Order(int.Parse(VendorsDisplay.SelectedValue));

            PurchaseOrderView.DataSource = newOrder;
            PurchaseOrderView.DataBind();

            List<PurchaseOrderList> inventory = new List<PurchaseOrderList>();
            inventory = Sysmgr.Get_currentInventory(int.Parse(VendorsDisplay.SelectedValue));

            CurrentInventoryView.DataSource = inventory;
            CurrentInventoryView.DataBind();

            GenerateAmounts(newOrder);
        }
    }

    public List<PurchaseOrderList> Get_listitemdata(int listtype)
    {
        List<PurchaseOrderList> list = new List<PurchaseOrderList>();
        if (listtype == 1)
        {
            IList<ListViewDataItem> data = PurchaseOrderView.Items;
            foreach (ListViewDataItem item in data)
            {
                PurchaseOrderList listitem = new PurchaseOrderList();
                listitem.itemid = int.Parse((item.FindControl("itemid") as Label).Text);
                listitem.Description = (item.FindControl("Description") as Label).Text;
                listitem.QOH = int.Parse((item.FindControl("QOH") as Label).Text);
                listitem.QOO = int.Parse((item.FindControl("ROL") as Label).Text);
                listitem.ROL = int.Parse((item.FindControl("QOO") as Label).Text);
                listitem.Price = decimal.Parse((item.FindControl("Price") as TextBox).Text);
                listitem.Quantity = int.Parse((item.FindControl("Quantity") as TextBox).Text);
                list.Add(listitem);
            }
            return list;
        }
        else
        {
            IList<ListViewDataItem> data = CurrentInventoryView.Items;
            foreach (ListViewDataItem item in data)
            {
                PurchaseOrderList listitem = new PurchaseOrderList();
                listitem.itemid = int.Parse((item.FindControl("itemid") as Label).Text);
                listitem.Description = (item.FindControl("Description") as Label).Text;
                listitem.QOH = int.Parse((item.FindControl("QOH") as Label).Text);
                listitem.QOO = int.Parse((item.FindControl("ROL") as Label).Text);
                listitem.ROL = int.Parse((item.FindControl("QOO") as Label).Text);
                listitem.Price = decimal.Parse((item.FindControl("Price") as Label).Text);
                listitem.Quantity = 0;
                list.Add(listitem);
            }
            return list;
        }
    }

    protected void ADD(object sender, ListViewCommandEventArgs a)
    {
        ListViewDataItem row = a.Item as ListViewDataItem;
        PurchaseOrderList newItem = new PurchaseOrderList();
        newItem.itemid = int.Parse((row.FindControl("itemid") as Label).Text);
        newItem.Description = (row.FindControl("Description") as Label).Text;
        newItem.QOH = int.Parse((row.FindControl("QOH") as Label).Text);
        newItem.QOO = int.Parse((row.FindControl("ROL") as Label).Text);
        newItem.ROL = int.Parse((row.FindControl("QOO") as Label).Text);
        newItem.Price = decimal.Parse((row.FindControl("Price") as Label).Text);
        newItem.Quantity = 0;

        int index = row.DataItemIndex;

        List<PurchaseOrderList> datatoberemoved = Get_listitemdata(0);
        List<PurchaseOrderList> datatobeadded = Get_listitemdata(1);

        datatoberemoved.RemoveAt(index);
        CurrentInventoryView.DataSource = datatoberemoved;
        CurrentInventoryView.DataBind();

        datatobeadded.Add(newItem);
        PurchaseOrderView.DataSource = datatobeadded;
        PurchaseOrderView.DataBind();

        GenerateAmounts(datatobeadded);
        show_everything();
    }

    protected void REMOVE(object sender, ListViewCommandEventArgs a)
    {
        ListViewDataItem row = a.Item as ListViewDataItem;
        PurchaseOrderList newItem = new PurchaseOrderList();
        newItem.itemid = int.Parse((row.FindControl("itemid") as Label).Text);
        newItem.Description = (row.FindControl("Description") as Label).Text;
        newItem.QOH = int.Parse((row.FindControl("QOH") as Label).Text);
        newItem.QOO = int.Parse((row.FindControl("ROL") as Label).Text);
        newItem.ROL = int.Parse((row.FindControl("QOO") as Label).Text);
        newItem.Price = decimal.Parse((row.FindControl("Price") as TextBox).Text);
        newItem.Quantity = 0;

        int index = row.DataItemIndex;

        List<PurchaseOrderList> datatobeadded = Get_listitemdata(0);
        List<PurchaseOrderList> datatoberemoved = Get_listitemdata(1);

        datatoberemoved.RemoveAt(index);
        PurchaseOrderView.DataSource = datatoberemoved;
        PurchaseOrderView.DataBind();

        datatobeadded.Add(newItem);
        CurrentInventoryView.DataSource = datatobeadded;
        CurrentInventoryView.DataBind();

        GenerateAmounts(datatoberemoved);
        show_everything();
    }

    public void hide_everything()
    {
        PurchaseOrderView.Visible = false;
        CurrentInventoryView.Visible = false;
        SubTotal.Visible = false;
        Tax.Visible = false;
        Amount.Visible = false;
        SubTotallabel.Visible = false;
        Taxlabel.Visible = false;
        Amountlabel.Visible = false;
        Update.Visible = false;
        Delete.Visible = false;
        Place.Visible = false;
        Clear.Visible = false;
    }
    public void show_everything()
    {
        PurchaseOrderView.Visible = true;
        CurrentInventoryView.Visible = true;
        SubTotal.Visible = true;
        Tax.Visible = true;
        Amount.Visible = true;
        SubTotallabel.Visible = true;
        Taxlabel.Visible = true;
        Amountlabel.Visible = true;
        Update.Visible = true;
        Delete.Visible = true;
        Place.Visible = true;
        Clear.Visible = true;
    }
    public void GenerateAmounts(List<PurchaseOrderList> data)
    {
        decimal subTotal = 0;

        foreach (PurchaseOrderList item in data)
        {
            subTotal = subTotal + item.Price * item.Quantity;
        }

        decimal gst = (subTotal * (decimal)0.05);
        decimal total = subTotal + gst;

        SubTotal.Text = subTotal.ToString();
        Tax.Text = gst.ToString();
        Amount.Text = total.ToString();
    }

    protected void Update_Click(object sender, EventArgs e)
    {
        List<PurchaseOrderList> updatefunctions = Get_listitemdata(1);
        PurchaseOrderController sysmgr = new PurchaseOrderController();
        if (updatefunctions.Any())
        {
            MessageUserControl.TryRun(() =>
            {
                string message = sysmgr.transaction(updatefunctions, 1, int.Parse(VendorsDisplay.SelectedValue), decimal.Parse(SubTotal.Text), decimal.Parse(Tax.Text));
                show_everything();
                GenerateAmounts(updatefunctions);
                VendorsDisplay.SelectedIndex = 0;
                MessageUserControl.ShowInfo(message);
            });
        }
        else
        {
            MessageUserControl.ShowInfo("No Order to update");
        }
    }

    protected void Delete_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            PurchaseOrderController sysmgr = new PurchaseOrderController();
            string message = sysmgr.transaction(null, 3, int.Parse(VendorsDisplay.SelectedValue), 0, 0);
            PurchaseOrderView.DataBind();
            CurrentInventoryView.DataBind();
            VendorsDisplay.SelectedIndex = 0;
            hide_everything();

            MessageUserControl.ShowInfo(message);
        });

    }

    protected void Place_Click(object sender, EventArgs e)
    {
        List<PurchaseOrderList> updatefunctions = Get_listitemdata(1);
        PurchaseOrderController sysmgr = new PurchaseOrderController();
        if (updatefunctions.Any())
        {
            MessageUserControl.TryRun(() =>
            {
                string message = sysmgr.transaction(updatefunctions, 2, int.Parse(VendorsDisplay.SelectedValue), decimal.Parse(SubTotal.Text), decimal.Parse(Tax.Text));
                show_everything();
                GenerateAmounts(updatefunctions);
                PurchaseOrderView.DataBind();
                CurrentInventoryView.DataBind();
                VendorsDisplay.SelectedIndex = 0;
                hide_everything();
                MessageUserControl.ShowInfo(message);
            });
        }
        else
        {
            MessageUserControl.ShowInfo("No Order to update");
        }
    }

    protected void Clear_Click(object sender, EventArgs e)
    {
        PurchaseOrderController sysmgr = new PurchaseOrderController();
        string message = sysmgr.transaction(null, 4, int.Parse(VendorsDisplay.SelectedValue), 0, 0);
        PurchaseOrderView.DataBind();
        CurrentInventoryView.DataBind();
        VendorsDisplay.SelectedIndex = 0;
        hide_everything();
        MessageUserControl.ShowInfo(message);
    }
}