<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Receiving.aspx.cs" Inherits="Pages_Receiving" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="col-md-12">
        <p style="float: right; padding-top: 2rem;">User: <%: Context.User.Identity.GetUserName()  %></p>
    </div>
    <div class="jumbotron">
        <h1>Receiving</h1>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>
    <div>
        <h4>Receiving: Order Listing</h4>
        <asp:GridView ID="OrderListGrid" CssClass="table table-bordered table-striped table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="OrderListODS">
            <Columns>
                <asp:TemplateField HeaderText="PurchaseOrderId" SortExpression="PurchaseOrderId" Visible="False">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("PurchaseOrderId") %>' ID="purchaseOrderID"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order" SortExpression="PurchaseOrderNumber">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("PurchaseOrderNumber") %>' ID="Label4"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order Date" SortExpression="OrderDate">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("OrderDate") %>' ID="Label3"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Vendor" SortExpression="VendorName">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("VendorName") %>' ID="Label2"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contact" SortExpression="VendorPhone">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Bind("VendorPhone") %>' ID="Label5"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text="View Order" CommandName="" OnClick="ViewOrder_Click" CausesValidation="false" ID="viewOrder"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <div class="row">
            <div class="col-md-12">
                <!--Nav tabs-->
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#receiving" data-toggle="tab">Receiving</a></li>
                    <li><a href="#unordered" data-toggle="tab">Unordered</a></li>
                </ul>
                <%--Tab panes one for each tab--%>
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane active" id="receiving">
                        <asp:UpdatePanel ID="ReceiveOrderPnl" runat="server">
                            <ContentTemplate>
                                <div class="row col-md-12">
                                    <div class="form-group" style="padding-bottom: 2rem; padding-top: 2rem;">
                                        <asp:Label CssClass="col-sm-1" ID="Label6" runat="server" Text="PO:"></asp:Label>
                                        <asp:Label CssClass="col-sm-2" ID="PONumber" runat="server"></asp:Label>
                                        <asp:Label CssClass="col-sm-1" ID="Label7" runat="server" Text="Vendor:"></asp:Label>
                                        <asp:Label CssClass="col-sm-2" ID="VendorName" runat="server"></asp:Label>
                                        <asp:Label CssClass="col-sm-1" ID="Label8" runat="server" Text="Contact:"></asp:Label>
                                        <asp:Label CssClass="col-sm-1" ID="Contact" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div>
                                    <asp:GridView ID="OrderDetailGrid" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="False" runat="server">
                                        <Columns>
                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Bind("PurchaseOrderId") %>' ID="purchaseOrderID"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Bind("PurchaseOrderDetailId") %>' ID="PurchaseOrderDetailId"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Part No">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Bind("PartId") %>' ID="partId"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Bind("PartDescription") %>' ID="description"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Ordered">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Bind("QtyOnOrder") %>' ID="qtyOnOrder"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Outstanding">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Bind("QtyOutstanding") %>' ID="qtyOutstanding"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receiving">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="receiving" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Returning">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="returning" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="reason" runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="unordered">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="UnorderedPartsList" runat="server" DataSourceID="UnorderedPartsODS" DataKeyNames="CartID" InsertItemPosition="FirstItem">
                                    <AlternatingItemTemplate>
                                        <tr style="">
                                            <td hidden>
                                                <asp:Label Text='<%# Eval("CartID") %>' runat="server" ID="CartIDLabel" Visible="False" /></td>
                                            <td>
                                                <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" />
                                            </td>
                                            <td>
                                                <asp:Label Text='<%# Eval("VendorPartNumber") %>' runat="server" ID="VendorPartNumberLabel" /></td>
                                            <td>
                                                <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                                            <td>
                                                <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <EditItemTemplate>
                                        <tr style="">
                                            <td hidden>
                                                <asp:TextBox Text='<%# Bind("CartID") %>' runat="server" ID="CartIDTextBox" Visible="False" /></td>
                                            <td>
                                                <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" />
                                            </td>
                                            <td>
                                                <asp:TextBox Text='<%# Bind("VendorPartNumber") %>' runat="server" ID="VendorPartNumberTextBox" /></td>
                                            <td>
                                                <asp:TextBox Text='<%# Bind("Quantity") %>' runat="server" ID="QuantityTextBox" /></td>
                                            <td>
                                                <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                                                <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
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
                                            <td hidden>
                                                <asp:TextBox Text='<%# Bind("CartID") %>' runat="server" ID="CartIDTextBox" Visible="False" /></td>
                                            <td>
                                                <asp:TextBox Text='<%# Bind("Description") %>' runat="server" ID="DescriptionTextBox" />
                                            </td>
                                            <td>
                                                <asp:TextBox Text='<%# Bind("VendorPartNumber") %>' runat="server" ID="VendorPartNumberTextBox" /></td>
                                            <td>
                                                <asp:TextBox Text='<%# Bind("Quantity") %>' runat="server" ID="QuantityTextBox" /></td>
                                            <td>
                                                <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                                                <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                                            </td>
                                        </tr>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <tr style="">

                                            <td hidden>
                                                <asp:Label Text='<%# Eval("CartID") %>' runat="server" ID="CartIDLabel" Visible="False" /></td>
                                            <td>
                                                <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                            <td>
                                                <asp:Label Text='<%# Eval("VendorPartNumber") %>' runat="server" ID="VendorPartNumberLabel" /></td>
                                            <td>
                                                <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                                            <td>
                                                <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <LayoutTemplate>
                                        <table runat="server" class="table table-bordered table-striped table-hover">
                                            <tr runat="server">
                                                <td runat="server">
                                                    <table class="table table-bordered" runat="server" id="itemPlaceholderContainer" style="" border="0">
                                                        <tr runat="server" style="">
                                                            <th runat="server" visible="False">CartID</th>
                                                            <th runat="server">Description</th>
                                                            <th runat="server">Vendor Part Number</th>
                                                            <th runat="server">Quantity</th>
                                                            <th runat="server"></th>
                                                        </tr>
                                                        <tr runat="server" id="itemPlaceholder"></tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td runat="server" style="">
                                                    <asp:DataPager runat="server" ID="DataPager1">
                                                        <Fields>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                            <asp:NumericPagerField></asp:NumericPagerField>
                                                            <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                    <SelectedItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                                            </td>
                                            <td>
                                                <asp:Label Text='<%# Eval("CartID") %>' runat="server" ID="CartIDLabel" /></td>
                                            <td>
                                                <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" /></td>
                                            <td>
                                                <asp:Label Text='<%# Eval("VendorPartNumber") %>' runat="server" ID="VendorPartNumberLabel" /></td>
                                            <td>
                                                <asp:Label Text='<%# Eval("Quantity") %>' runat="server" ID="QuantityLabel" /></td>
                                        </tr>
                                    </SelectedItemTemplate>
                                </asp:ListView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row" style="padding-top: 1rem">
                    <asp:Button ID="receiveBtn" runat="server" Text="Receive" OnClick="receiveBtn_Click" Enabled="False" />
                    <asp:Button ID="forceCloserBtn" runat="server" Text="Force Closer" OnClick="forceCloserBtn_Click" Enabled="False" />
                    <asp:TextBox ID="reasonClose" runat="server" Enabled="False"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <asp:ObjectDataSource ID="OrderListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Get_OutstandingOrders" TypeName="eBike.System.BLL.ReceivingOrderController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="UnorderedPartsODS" runat="server"
        DataObjectTypeName="eBike.Data.Entities.UnorderedPurchaseItemCart"
        SelectMethod="Get_UnorderedParts"
        DeleteMethod="Delete_UnOrderedParts"
        InsertMethod="Add_UnorderedPart" OldValuesParameterFormatString="original_{0}"
        TypeName="eBike.System.BLL.ReceivingOrderController"
        OnSelected="CheckForException"
        OnDeleted="CheckForException"
        OnInserted="CheckForException"></asp:ObjectDataSource>
</asp:Content>

