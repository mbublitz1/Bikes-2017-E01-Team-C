<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="Pages_Sales_ShoppingCart" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="jumbotron" id="shopping-cart">
        <h1>Shopping Cart:
            <asp:Label ID="CurrentUser" runat="server" Text=""></asp:Label></h1>
        <a runat="server" href="~/Pages/Sales/ShoppingCart.aspx">
            <img src="/img/shopping-cart-icon.png" class="jumbo-cart-icon" /></a>
            <asp:Label ID="CurretyItemsQtyCart" runat="server" Text="" CssClass="current-number-in-cart-label"></asp:Label>
    </div>

    <div class="row">
        <div class="col-sm-12">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>

    <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
            <asp:ListView ID="ShoppingCartItemListView" runat="server" DataSourceID="ShoppingCartItemODS">
                <EmptyDataTemplate>
                    <table runat="server" style="">
                        <tr>
                            <td>There are no items in you shopping cart. <a runat="server" href="~/Pages/Sales/PartsCatalog.aspx">Go to Parts Catalog</a></td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <tr style="">        
                        <td class="cart-item-td">
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" class="cart-description"/>
                            <asp:Label Text='<%# "Unit Price: $" + Eval("UnitPrice", "{0:0.00}") %>' runat="server" ID="UnitPriceLabel" class="cart-unit-price"/>
                            <asp:Label ID="BackOrder" runat="server" Text='<%# Eval("Backorder") %>' CssClass="backorder-shoppingcart"></asp:Label>
                        </td>
                        <td class="cart-qty-td">     
                            <asp:LinkButton ID="SubtractQtyFromItem" runat="server" CssClass="subtract-item-from-cart" CommandArgument="Subtract" OnClick="UpdateQty_Click">-</asp:LinkButton>
                            <asp:Label Text='<%# Eval("Qty") %>' runat="server" ID="QtyLabel" CssClass="current-item-qty"/>
                            <asp:LinkButton ID="AddQtyToItem" runat="server" CssClass="add-item-to-cart" CommandArgument="Add" OnClick="UpdateQty_Click">+</asp:LinkButton>
                        </td>
                        <td class="cart-item-total">
                            <asp:Label Text='<%# "$" + Eval("ItemTotal", "{0:0.00}") %>' runat="server" ID="ItemTotalLabel" class="cart-item-total-label"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("ShoppingCartItemId") %>' runat="server" ID="ProductIdLabel" Visible="false"/>
                            <asp:LinkButton ID="DeleteItemBtn" runat="server" CommandArgument='<%# Eval("ShoppingCartItemId") %>' OnClick="DeleteItemBtn_Click">
                                <img src="/img/trash_can_cart.png" />
                            </asp:LinkButton>
                            </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server" id="itemPlaceholderContainer" style="" border="0" class="shopping-cart-table">
                        <tr runat="server" style="" visible="false">                                  
                            <th runat="server">Description & UnitPrice & Backorder</th>
                            <th runat="server">Qty</th>
                            <th runat="server">ItemTotal</th>
                            <th runat="server">ShoppingCartItemIdd</th>
                        </tr>
                        <tr runat="server" id="itemPlaceholder"></tr>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="ShoppingCartItemODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_ShoppingCartItems" TypeName="eBike.System.BLL.Sales.ShoppingCartController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="CurrentUser" PropertyName="Text" Name="username" Type="String"></asp:ControlParameter>

                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
            <div class="place-order">
                <h2>Checkout | <asp:Label ID="CurrentCartTotal" runat="server" Text=""></asp:Label></h2>            
                <a runat="server" id="checkoutBtn" href="~/Pages/Sales/Checkout.aspx" class="proceed-to-order">
                    <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                </a>          
            </div>
        </div>
    </div>
</asp:Content>

