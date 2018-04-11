<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SaleConfirmation.aspx.cs" Inherits="Pages_Sales_SaleConfirmation" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="jumbotron" id="checkout-jumbo">
        <h1>Sale Confirmation</h1>
        <asp:Label ID="currentSale" runat="server" Text="" Visible="false"></asp:Label>
        <div class="col-sm-12">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12">
            <div class="sale-confirmation">
                <asp:ListView ID="SaleInfoLV" runat="server" DataSourceID="SaleConfirmationODS">
                    <EmptyDataTemplate>
                        <span>No sale was found.</span>
                    </EmptyDataTemplate>       
                    <ItemTemplate>
                        <div class="row saleinfo-cont">
                            <div class="col-sm-4">
                                <span class="salelbl">Sale ID:</span>
                                <asp:Label Text='<%# Eval("SaleID") %>' runat="server" ID="SaleIDLabel" />
                            </div>
                            <div class="col-sm-4">
                                <span class="salelbl">Sale Date:</span>
                                <asp:Label Text='<%# Eval("SaleDate") %>' runat="server" ID="SaleDateLabel" />
                            </div>
                            <div class="col-sm-4">
                                <span class="salelbl">Username:</span>
                                <asp:Label Text='<%# Eval("UserName") %>' runat="server" ID="UserNameLabel" /><br />
                            </div>
                        </div>
                        <div class="row saleinfo-cont">
                            <div class="col-sm-4">
                                <span class="salelbl">Total:</span>
                                <asp:Label Text='<%# Eval("SubTotal", "{0:C2}") %>' runat="server" ID="SubTotalLabel" />
                            </div>
                            <div class="col-sm-4">
                                <span class="salelbl">Coupon ID:</span>
                                <asp:Label Text='<%# Eval("CouponID") %>' runat="server" ID="CouponIDLabel" />
                            </div>
                            <div class="col-sm-4">
                                <span class="salelbl">Payment Type:</span>
                                <asp:Label Text='<%# Eval("PaymentType") %>' runat="server" ID="PaymentTypeLabel" />
                            </div>
                        </div>  
                    </ItemTemplate>
                    <LayoutTemplate>
                        <div runat="server" id="itemPlaceholderContainer" style=""><span runat="server" id="itemPlaceholder" /></div>
                        <div style="">
                        </div>
                    </LayoutTemplate> 
                </asp:ListView>
                <asp:ObjectDataSource ID="SaleConfirmationODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetSaleConfirmation" TypeName="eBike.System.BLL.Sales.SalesController" >
                    <SelectParameters>
                        <asp:ControlParameter ControlID="currentSale" PropertyName="Text" Name="saleid" Type="Int32"></asp:ControlParameter>
                    </SelectParameters>
                </asp:ObjectDataSource>


                <asp:ListView ID="SaleDetailsListView" runat="server" DataSourceID="SaleDetaileListViewODS">
                    <EmptyDataTemplate>
                        <table runat="server" style="">
                            <tr>
                                <td>No items in the cart.</td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    
                    <ItemTemplate>
                        <tr style="">
                            <td>
                                <asp:Label Text='<%# Eval("SaleDetailID") %>' runat="server" ID="SaleDetailIDLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("PartID") %>' runat="server" ID="PartIDLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("PartName") %>' runat="server" ID="PartNameLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("Qty") %>' runat="server" ID="QtyLabel" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("SellingPrice", "{0:C2}") %>' runat="server" ID="SellingPriceLabel" /></td>
                            <td>
                                <asp:CheckBox Checked='<%# Eval("BackOrder") %>' runat="server" ID="BackOrderCheckBox" Enabled="false" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("shippedDate") %>' runat="server" ID="shippedDateLabel" /></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table runat="server" class="saleItemContainertable">
                            <tr runat="server">
                                <td runat="server">
                                    <table runat="server" id="itemPlaceholderContainer" style="" border="0" class="saleItem-table">
                                        <tr runat="server" style="">
                                            <th runat="server">Detail ID</th>
                                            <th runat="server">ID</th>
                                            <th runat="server">Description</th>
                                            <th runat="server">Qty</th>
                                            <th runat="server">Unit Price</th>
                                            <th runat="server">Back Order</th>
                                            <th runat="server">Shipped Date</th>
                                        </tr>
                                        <tr runat="server" id="itemPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr runat="server">
                                <td runat="server" style=""></td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="SaleDetaileListViewODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetSaleItemDetails" TypeName="eBike.System.BLL.Sales.SaleDetailController">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="currentSale" PropertyName="Text" Name="saleid" Type="Int32"></asp:ControlParameter>
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>

</asp:Content>

