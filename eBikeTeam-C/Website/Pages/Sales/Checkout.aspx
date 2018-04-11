<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Checkout.aspx.cs" Inherits="Pages_Sales_Checkout" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="jumbotron" id="checkout-jumbo">
        <h1>Checkout</h1>
        <asp:Label ID="currentUser" runat="server" Text="" Visible="false"></asp:Label>
        <div class="col-sm-12">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
    <div class="row">
        <div class="col-sm-8 col-sm-offset-2">
            <script>
                function nextButton(anchorRef) {
                    $('a[href="' + anchorRef + '"]').tab('show');
                }
            </script>
            <div class="checkout-container">
                <ul class="nav nav-tabs" id="checkout-tabs">
                    <li class="active"><a href="#shipping" data-toggle="tab">Shipping</a></li>
                    <li><a href="#payment" data-toggle="tab">Payment</a></li>
                    <li><a href="#confirmation" data-toggle="tab">Confirmation</a></li>
                </ul>

                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane active" id="shipping">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                            <ContentTemplate>
                                <div class="checkout-form">
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:Label ID="fname" runat="server" Text="First Name" CssClass="checkout-lbl" AssociatedControlID="fnameInput"></asp:Label>
                                                <asp:TextBox ID="fnameInput" runat="server" CssClass="form-control" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:Label ID="lname" runat="server" Text="Last Name" CssClass="checkout-lbl" AssociatedControlID="lnameInput"></asp:Label>
                                                <asp:TextBox ID="lnameInput" runat="server" CssClass="form-control" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:Label ID="phoneNumber" runat="server" Text="Phone Number" CssClass="checkout-lbl" AssociatedControlID="phoneNumberInput"></asp:Label>
                                                <asp:TextBox ID="phoneNumberInput" runat="server" CssClass="form-control" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:Label ID="email" runat="server" Text="E-Mail" CssClass="checkout-lbl" AssociatedControlID="emailInput"></asp:Label>
                                                <asp:TextBox ID="emailInput" runat="server" CssClass="form-control" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <asp:Label ID="address" runat="server" Text="Address" CssClass="checkout-lbl" AssociatedControlID="addressInput"></asp:Label>
                                                <asp:TextBox ID="addressInput" runat="server" CssClass="form-control" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:Label ID="city" runat="server" Text="City" CssClass="checkout-lbl" AssociatedControlID="cityInput"></asp:Label>
                                                <asp:TextBox ID="cityInput" runat="server" CssClass="form-control" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:Label ID="provinceState" runat="server" Text="Province | State" CssClass="checkout-lbl" AssociatedControlID="provinceStateInput"></asp:Label>
                                                <asp:TextBox ID="provinceStateInput" runat="server" CssClass="form-control" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:Label ID="country" runat="server" Text="Country" CssClass="checkout-lbl" AssociatedControlID="countryInput"></asp:Label>
                                                <asp:TextBox ID="countryInput" runat="server" CssClass="form-control" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-xs-6">
                                            <div class="form-group">
                                                <asp:Label ID="postalCode" runat="server" Text="Postal Code" CssClass="checkout-lbl" AssociatedControlID="postalCodeInput"></asp:Label>
                                                <asp:TextBox ID="postalCodeInput" runat="server" CssClass="form-control" ViewStateMode="Enabled"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>  
                                </div>
                                
                                <div class="next-step-container">
                                    <h2>Continue to Payment</h2>
                                    <linkbutton id="toPaymnetBtn" type="button" class="proceed-to" onclick="nextButton('#payment')">
                                        <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                    </linkbutton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="payment">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">

                            <ContentTemplate>
                                <div class="promoCode-container">
                                    <asp:Label ID="promoCodeLbl" runat="server" Text="Promo Code" CssClass="promoCode-label"></asp:Label>
                                    <asp:DropDownList ID="PromoCodeDropDownList" runat="server" CssClass="promoCode-textbox" AppendDataBoundItems="true" OnSelectedIndexChanged="PromoCodeDropDownList_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Select...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="paymentType-container">
                                    <asp:RadioButtonList ID="PaymentTypeList" runat="server" RepeatLayout="UnorderedList" CssClass="paymentTypeTable"  OnSelectedIndexChanged="PaymentTypeList_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="c" Selected="True">
                                            <h3 class="payment-type-name">Credit</h3>
                                            <img src="../../img/paymnet-credit.png" class="payment-type-icon" />
                                        </asp:ListItem>
                                        <asp:ListItem Value="d">
                                            <h3 class="payment-type-name">Debit</h3>
                                            <img src="../../img/paymnet-debit.png" class="payment-type-icon" />
                                        </asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

                                <div class="paymentInfo-container">
                                    <div class="dicount-percent-cont">
                                        <asp:Label ID="DsicountPercent" runat="server" Text="Discount" CssClass="discount-percent-lbl"></asp:Label>
                                        <asp:Label ID="DiscountPercentValue" runat="server" Text="0%" CssClass="discount-percent-value-lbl"></asp:Label>
                                    </div>
                                    <div class="dicount-cont">
                                        <asp:Label ID="DiscountText" runat="server" Text="Discount Total" CssClass="discount-text-lbl"></asp:Label>
                                        <asp:Label ID="DiscountValue" runat="server" Text="$0.00" CssClass="discount-value-lbl"></asp:Label>
                                    </div>
                                    <div class="total-cont">
                                        <asp:Label ID="TotalText" runat="server" Text="Sub Total" CssClass="total-text-lbl"></asp:Label>
                                        <asp:Label ID="TotalValue" runat="server" Text="" CssClass="total-value-lbl"></asp:Label>
                                    </div>
                                </div>
                                <div class="next-step-container">
                                    <h2>Continue to Confirmation</h2>
                                    <linkbutton id="toConfirmationbtn" type="button" class="proceed-to" onclick="nextButton('#confirmation')">
                                        <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                    </linkbutton>

                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="confirmation">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">

                            <ContentTemplate>
                                <div class="checkout-info-container">
                                <asp:Repeater ID="CheckoutRepeaterView" runat="server" DataSourceID="CkeckoutInformationODS" ItemType="eBike.Data.DTOs.ShoppingCartCheckoutDTO">
                                    <HeaderTemplate>
                                        <h4 class="order-deatils-h4">Order Details</h4>
                                        <table class="checkout-info-table">
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="2" class="items-table-td">
                                        <asp:Repeater runat="server" ID="CheckoutRepeaterItems" DataSource='<%# Item.ShoppingCartItems %>'
                                            ItemType="eBike.Data.POCOs.ShoppingCartLineItemPOCO">
                                            <HeaderTemplate>
                                                <table class="checkout-items-table">
                                                    <tbody>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="checkout-desc">
                                                        <%# Item.Description %>
                                                    </td>
                                                    <td class="checkout-item-total">
                                                        <%# string.Format("{0:C2}", Item.ItemTotal) %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="itm-info-pd">
                                                        <span class="checkout-item-qty">
                                                            <%# "Qty: " + Item.Qty %>  
                                                        </span>
                                                        <span class="checkout-unitprice">
                                                            <%# "Unit Price: " + string.Format("{0:C2}", Item.UnitPrice) %>
                                                        </span>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </tbody>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                           </td>
                                        </tr>
                                        <tr>
                                            <td class="checkout-payment-type">
                                                Payment Type
                                            </td>
                                            <td class="checkout-payment-type-value">
                                                <asp:Label ID="SalePayType" runat="server" Text="Credit" CssClass="sale-payment-type-value"></asp:Label>  
                                                <asp:Image ID="SalePatImg" runat="server" CssClass="sale-payment-type-img" ImageUrl="/img/paymnet-credit.png"/>                                                 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="checkout-subtotal">
                                                Sub Total
                                            </td>
                                            <td class="checkout-subtotal-value">
                                                <%# string.Format("{0:C2}", Item.SubTotal) %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="checkout-discount">
                                                Discount
                                            </td>
                                            <td class="checkout-discount-value">
                                                <asp:Label ID="SaleDiscountValue" runat="server" Text="$0.00"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="checkout-total">
                                                Total
                                            </td>
                                            <td class="checkout-total-value">
                                                <asp:Label ID="SaleFinalTotal" runat="server" Text='<%# string.Format("{0:C2}", Item.Total) %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </tbody>
                                        </table>
                                    </FooterTemplate>

                                </asp:Repeater>
                                <asp:ObjectDataSource ID="CkeckoutInformationODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCurrentCheckoutInfo" TypeName="eBike.System.BLL.Sales.ShoppingCartController">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="currentUser" PropertyName="Text" Name="username" Type="String"></asp:ControlParameter>
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <div class="next-step-container">
                                    <h2>Place Order</h2>
                                    <asp:LinkButton ID="CreateSaleBtn" runat="server" OnClick="CreateSaleBtn_Click" CssClass="proceed-to">
                                        <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                                    </asp:LinkButton>
                                </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>


            </div>
        </div>
    </div>
</asp:Content>

