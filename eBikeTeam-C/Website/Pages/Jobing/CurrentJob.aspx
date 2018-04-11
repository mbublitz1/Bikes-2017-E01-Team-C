<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CurrentJob.aspx.cs" Inherits="Jobing_CurrentJob" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
  <div class="jumbotron">
  <h1>Current Job</h1>    
  </div>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <div class ="row">
    <div class="col-md-2">
    <asp:Label  runat="server" Text="User: "  AssociatedControlID="UserFullName"></asp:Label>
    <asp:Label ID="UserFullName" runat="server"></asp:Label>
        </div>
        &nbsp&nbsp&nbsp
        <div class ="col-md-1">
             <asp:Label  runat="server" Text="Job: " AssociatedControlID="JobID"></asp:Label>
    <asp:Label ID="JobID" runat="server" ></asp:Label>
        </div> 
        &nbsp&nbsp
        <div class="col-md-4">
            <asp:Label  runat="server" Text="Customer: " AssociatedControlID="CustomerName"></asp:Label>
    <asp:TextBox ID="CustomerName" runat="server"></asp:TextBox>
        </div>
         &nbsp
        <div class="col-md-5">
                  <asp:Label  runat="server" Text="Contact: " AssociatedControlID="ContactNumber"></asp:Label>
    <asp:TextBox ID="ContactNumber" runat="server"></asp:TextBox>
        </div>
     </div>
    <div class="row">
        <div class="col-md-5">
             <asp:Label  runat="server" Text="Presets: " AssociatedControlID="PresetDDL"></asp:Label>
            <asp:DropDownList ID="PresetDDL" runat="server" DataSourceID="PresetODS" DataTextField="Description" DataValueField="StandardJobID"  >
            </asp:DropDownList>
            <asp:Button ID="PresetButton" runat="server" Text="Select" OnClick="PresetButton_Click" />
        </div>
        <div class="col-md-3">
            <asp:Label runat="server" Text="Coupon: " AssociatedControlID="CouponDDL"></asp:Label>
            <asp:DropDownList ID="CouponDDL"  AppendDataBoundItems="true" runat="server" DataSourceID="CouponODS" DataTextField="CouponIDValue" DataValueField="CouponID">
                <asp:ListItem Value="0" Enabled="true"  Selected="True">Select....</asp:ListItem>
               
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <asp:Button ID="AddServiceButton" runat="server" Text="Add Service" OnClick="AddServiceButton_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <asp:Label  AssociatedControlID="Description" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="Description" runat="server" Text="" Width="250px"></asp:TextBox>
        </div>
        <div class ="col-md-4">
            <asp:Label  AssociatedControlID="Hours" runat="server" Text="Hours: "></asp:Label>
            <asp:TextBox ID="Hours" runat="server" ></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
        <asp:Label  AssociatedControlID="Comments" runat="server" Text="Comments: "></asp:Label>
        <asp:TextBox ID="Comments" runat="server" Height="100" Width="800px"></asp:TextBox>
            </div>
    </div>
    <br />
    <br />
    <div class ="row">
    <asp:GridView ID="ServiceList" runat="server" AutoGenerateColumns="False" DataSourceID="ServiceODS" OnRowCommand="ServiceList_RowCommand" >
        <Columns>
            <asp:TemplateField  SortExpression="ServiceID">
                <ItemTemplate>
                    <asp:Button runat="server" Text="Remove"  CommandName="RemoveService" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ID='RemoveButton'
                        />
                    <asp:Label runat="server" ID="ServiceID" Text='<%# Eval("ServiceID") %>' Visible ="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>
            <asp:BoundField DataField="Hours" HeaderText="Hours" SortExpression="Hours"></asp:BoundField>
            <asp:BoundField DataField="Coupon" HeaderText="Coupon" SortExpression="Coupon"></asp:BoundField>
            <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments"></asp:BoundField>
        </Columns>
        <EmptyDataTemplate>
            No Services Found
        </EmptyDataTemplate>
    </asp:GridView>
        <br />
        <asp:Button ID="StartJob" runat="server" Text="Start Jobs" OnClick="StartJob_Click" />
    </div>
    <asp:ObjectDataSource ID="ServiceODS" runat="server" OldValuesParameterFormatString="original_{0}" TypeName="eBike.System.BLL.Jobing.JobDetailsController" OnSelected="CheckForException" SelectMethod="List_JobService">
        <SelectParameters>
            <asp:ControlParameter ControlID="JobID" PropertyName="Text" Name="jobID"  Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>



    <asp:ObjectDataSource ID="PresetODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_Preset" TypeName="eBike.System.BLL.Jobing.StandardJobController" OnSelected="CheckForException"></asp:ObjectDataSource>

    <asp:ObjectDataSource ID="CouponODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_Coupon" TypeName="eBike.System.BLL.Jobing.CouponController"
         OnSelected="CheckForException"></asp:ObjectDataSource>
</asp:Content>

