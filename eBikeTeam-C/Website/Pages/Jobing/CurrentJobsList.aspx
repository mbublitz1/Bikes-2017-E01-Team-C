<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CurrentJobsList.aspx.cs" Inherits="Jobing_CurrentJobList" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

    <div class="jumbotron">
        <h1>Current Job List</h1>
    </div>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
<div class ="row">
    <div class="col-md-6">
    <asp:Label ID="Label1" runat="server" Text="User: "></asp:Label>
    <asp:Label ID="UserFullName" runat="server" ></asp:Label>
        </div>
     <div class ="col-md-6">
        <asp:Button ID="NewJobButton" runat="server" Text="New Job" OnClick="NewJobButton_Click" />
    </div>
</div>
    <br />
    <div class="row">
        <asp:GridView ID="CurrentJobList" runat="server" AutoGenerateColumns="False" DataSourceID="CurrentJobListODS"
            OnRowCommand="CurrentJobsList_RowCommand">
            <Columns>
                <asp:BoundField DataField="JobID" HeaderText="JobID" SortExpression="JobID"></asp:BoundField>
                <asp:BoundField DataField="In" HeaderText="In" SortExpression="In" 
                     DataFormatString="{0:MMM/dd/yyyy}"></asp:BoundField>
                <asp:BoundField DataField="Started" HeaderText="Started" SortExpression="Started"
                    DataFormatString="{0:MMM/dd/yyyy}"></asp:BoundField>
                <asp:BoundField DataField="Done" HeaderText="Done" SortExpression="Done"
                    DataFormatString="{0:MMM/dd/yyyy}"></asp:BoundField>
                <asp:BoundField DataField="Customer" HeaderText="Customer" SortExpression="Customer"></asp:BoundField>
                <asp:BoundField DataField="ContactNumber" HeaderText="ContactNumber" SortExpression="ContactNumber"></asp:BoundField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="ViewCurrentJob" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Text="View"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="CurrentJobListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_CurrentJobs" 
           OnSelected="CheckForException" TypeName="eBike.System.BLL.Jobing.JobController"></asp:ObjectDataSource>
    </div>
</asp:Content>

