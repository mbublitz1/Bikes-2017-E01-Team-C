<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="CurrentServiceDetails.aspx.cs" Inherits="Jobing_CurrentServiceDetails" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="jumbotron">
        <h1>Current Job Service Details</h1>
    </div>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <div class="row">
        <div class="col-md-3">
        <asp:Label AssociatedControlID="UserFullName" runat="server" Text="User: "></asp:Label>
        <asp:Label ID="UserFullName" runat="server"></asp:Label>
            </div>
        <div class="col-md-4">
            <asp:Label  AssociatedControlID="JobNumber" runat="server" Text="Job: "></asp:Label>
            <asp:Label ID="JobNumber" runat="server" ></asp:Label>
        </div>
        <div class="col-md-5">
            <asp:Label AssociatedControlID="CustomerName" runat="server" Text="Customer: "></asp:Label>
            <asp:Label ID="CustomerName" runat="server" ></asp:Label>
            <asp:Label AssociatedControlID="ContactNumber" runat="server" Text="Contact: "></asp:Label>
            <asp:Label ID="ContactNumber" runat="server" ></asp:Label>
        </div>
    </div>
    <br />
    <br />
    <div class="row">
        <div class="col-md-3">
            <h3>Services</h3>
        </div>
        <div class="col-md-3">
            <asp:Button ID="StartServiceButton" runat="server" Text="Start Service" OnClick="StartServiceButton_Click" />
        </div>
    </div>
    <div class="row">
        <asp:GridView ID="ServiceStatus" runat="server" AutoGenerateColumns="False" OnRowCommand="ServiceStatus_RowCommand" DataSourceID="ServiceStatusODS">
            <Columns>
                <asp:BoundField DataField="JobDetailID" HeaderText="JobDetailID" SortExpression="JobDetailID"></asp:BoundField>
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description"></asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status"></asp:BoundField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="SelectButton" runat="server" CausesValidation="false" CommandName="Select" Text="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="DoneButton" runat="server" CausesValidation="false" CommandName="Done" Text="Done" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="RemoveButton" runat="server" CausesValidation="false" CommandName="Remove" Text="Remove"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                Job must be selected first
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <div class="row">
        <div class="col-md-7">
            <asp:Label AssociatedControlID="Description" runat="server" Text="Description: "></asp:Label>
            <asp:Label ID="Description" runat="server"></asp:Label>
        </div>
        <div class="col-md-5">
            <asp:Label AssociatedControlID="Hours" runat="server" Text="Hours: "></asp:Label>
            <asp:Label ID="Hours" runat="server"></asp:Label>
        </div>
    </div>
    <div class="row">
        <asp:Label AssociatedControlID="Comments" runat="server" Text="Comments: "></asp:Label>
        <asp:Label ID="Comments" runat="server"></asp:Label>

    </div>
    <div class="row">
        <div class="col-md-3">
            <asp:Button ID="AdditionalCommentButton" runat="server" Text="Add" />
        </div>
        <div class="col-md-9">
            <asp:TextBox ID="AdditonalComments" runat="server" Width="600px"></asp:TextBox>
        </div>
    </div>
    <asp:ListView ID="JobPartList" runat="server" DataSourceID="JobPartODS">
       
        <EditItemTemplate>

            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:TextBox ID="PartIDTextBox" runat="server" Text='<%# Bind("PartID") %>' />
                </td>
                <td>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                </td>
                <td>
                    <asp:TextBox ID="QtyTextBox" runat="server" Text='<%# Bind("Qty") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>
                    <asp:TextBox ID="PartIDTextBox" runat="server" Text='<%# Bind("PartID") %>' />
                </td>
                <td>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                </td>
                <td>
                    <asp:TextBox ID="QtyTextBox" runat="server" Text='<%# Bind("Qty") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Label ID="PartIDLabel" runat="server" Text='<%# Eval("PartID") %>' />
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                </td>
                <td>
                    <asp:Label ID="QtyLabel" runat="server" Text='<%# Eval("Qty") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr runat="server" style="">
                                <th runat="server">PartID</th>
                                <th runat="server">Description</th>
                                <th runat="server">Qty</th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                <asp:NumericPagerField />
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>

    </asp:ListView>
    <asp:ObjectDataSource ID="ServiceStatusODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_JobDetailStatus" TypeName="eBike.System.BLL.Jobing.JobDetailsController" OnSelected="CheckForException">
        <SelectParameters>
            <asp:ControlParameter ControlID="JobNumber" PropertyName="Text" Name="jobID" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="JobPartODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_JobDetailParts" TypeName="eBike.System.BLL.Jobing.JobDetailPartsController">
        <SelectParameters>
            <asp:ControlParameter ControlID="ServiceStatus" Name="jobdetailid" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

