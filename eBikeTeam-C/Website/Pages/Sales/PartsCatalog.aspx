<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PartsCatalog.aspx.cs" Inherits="Pages_Sales" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="jumbotron" id="parts-cat">
        <h1>Parts Catalog</h1>
        <a runat="server" href="~/Pages/Sales/ShoppingCart.aspx"><img src="/img/shopping-cart-icon.png" class="jumbo-cart-icon" /></a>
        <asp:Label ID="CurretyItemsQtyCart" runat="server" Text="" CssClass="current-number-in-cart-label"></asp:Label>
        <asp:Label runat="server" ID="CurrentCategoryId" Text="0" Visible="false"></asp:Label>
        <asp:Label runat="server" ID="CurrentLoggedInUser" Text="" Visible="false"></asp:Label>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>

    <div class="row">
        <div class="col-sm-3 col-sm-offset-1 category-filter">
            <h2>Search by Category</h2>
            <asp:ListView ID="AllCategoriesList" runat="server" DataSourceID="AllCategoriesListODS">
                <EmptyDataTemplate>
                    <span>No data was returned.</span>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <span style="">
                        <asp:Label Text='<%# Eval("CategoryID") %>' runat="server" AssociatedControlID="CategoryFilterAll" ID="CategoryIDLabel" Visible="false" />
                        <asp:LinkButton ID="CategoryFilterAll" runat="server" CssClass="categoryfilter-btn" OnClick="CategoryFilter_Click" CommandArgument='<%# Eval("CategoryID") %>'>
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" CssClass="category-description" />
                            <asp:Label Text='<%# Eval("PartCount") %>' runat="server" ID="PartCountLabel" CssClass="part-count" />
                        </asp:LinkButton></span>
                </ItemTemplate>
                <LayoutTemplate>
                    <div runat="server" id="itemPlaceholderContainer" style=""><span runat="server" id="itemPlaceholder" /></div>
                    <div style="">
                    </div>
                </LayoutTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="AllCategoriesListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_All_Count" TypeName="eBike.System.BLL.Sales.CategoryController"></asp:ObjectDataSource>

            <asp:ListView ID="CategoryFilterList" runat="server" DataSourceID="CategoryFilterListODS">
                <EmptyDataTemplate>
                    <span>No data was returned.</span>
                </EmptyDataTemplate>
                <ItemTemplate>
                    <span style="">
                        <asp:Label Text='<%# Eval("CategoryID") %>' runat="server" AssociatedControlID="CategoryFilter1" ID="CategoryIDLabel" Visible="false" />
                        <asp:LinkButton ID="CategoryFilter1" runat="server" CssClass="categoryfilter-btn" OnClick="CategoryFilter_Click" CommandArgument='<%# Eval("CategoryID") %>' EnableViewState="true">
                            <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="DescriptionLabel" CssClass="category-description" />
                            <asp:Label Text='<%# Eval("PartCount") %>' runat="server" ID="PartCountLabel" CssClass="part-count" />
                        </asp:LinkButton></span>
                </ItemTemplate>
                <LayoutTemplate>
                    <div runat="server" id="itemPlaceholderContainer" style=""><span runat="server" id="itemPlaceholder" /></div>
                    <div style="">
                    </div>
                </LayoutTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="CategoryFilterListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_Categories_For_Filter" TypeName="eBike.System.BLL.Sales.CategoryController"></asp:ObjectDataSource>
        </div>
        <div class="col-sm-7 product-list">
            <h2>Products:
                <asp:Label runat="server" ID="catFilterType" Text="All Categories"></asp:Label></h2>
            <asp:LoginView runat="server" ID="ProductsLoggedIn">
                <AnonymousTemplate>
                    <asp:GridView ID="ProductListByCatAnon" runat="server" AutoGenerateColumns="False" DataSourceID="ProductListByCatODS" AllowPaging="True" CssClass="parts-table" PageSize="7" PagerStyle-CssClass="page-container">
                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" FirstPageText="First" LastPageText="Last" PageButtonCount="3" />
                        <Columns>
                            <asp:TemplateField SortExpression="PartId">
                                <ItemTemplate>
                                    <div class="add_to_cart">
                                        <asp:Label runat="server" Text='<%# Bind("PartId") %>' ID="PartIdLabel" Visible="false"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="CartQty">
                                <ItemTemplate>

                                    <div class="login-to-shop">
                                        <a runat="server" href="~/Account/Login"><span class="glyphicon glyphicon-user" aria-hidden="true"></span>Log in</a>
                                        <asp:Label runat="server" Text='<%# Bind("CartQty") %>' ID="CartQtyLabel" Visible="false"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="PartName">
                                <ItemTemplate>
                                    <div class="part-description">
                                        <asp:Label runat="server" Text='<%# Bind("PartName") %>' ID="PartNameLabel"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="UnitPrice">
                                <ItemTemplate>
                                    <div class="product-unit-price">
                                        <asp:Label runat="server" Text='<%# "$" + Eval("UnitPrice", "{0:0.00}") %>' ID="UnitPriceLabel"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="QtyInStock">
                                <ItemTemplate>
                                    <div class="stock-amount">
                                        <asp:Label runat="server" ID="QtyInStockLabel"><span class="stock-span">Stock:</span> <%# Eval("QtyInStock") %></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </AnonymousTemplate>
                <LoggedInTemplate>

                    <asp:GridView ID="ProductListByCatLoged" runat="server" AutoGenerateColumns="False" DataSourceID="ProductListByCatODS" AllowPaging="True" CssClass="parts-table" PageSize="7" PagerStyle-CssClass="page-container">
                        <PagerSettings Mode="NumericFirstLast" Position="Bottom" FirstPageText="First" LastPageText="Last" PageButtonCount="3" />
                        <Columns>
                            <asp:TemplateField SortExpression="PartId">
                                <ItemTemplate>
                                    <div class="add_to_cart">
                                        <asp:Label runat="server" Text='<%# Bind("PartId") %>' ID="PartIdLabel" Visible="false"></asp:Label>
                                        <asp:LinkButton ID="AddToCartBtn" runat="server" CommandName="addToCart" CommandArgument='<%# Eval("PartId") %>' CssClass="add-to-shopping-cart" OnClick="AddToCartBtn_Click">
                                            <img src="/img/add_to_cart_icon.png" />
                                            <asp:Label runat="server" Text='<%# Bind("CartQty") %>' ID="CartQtyLabel" CssClass="addQty-Amount"></asp:Label>
                                        </asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="AddQty">
                                <ItemTemplate>
                                    <div class="qty_in_cart">
                                        <asp:Label runat="server" Text="" ID="AddQtyLbl" Visible="false"></asp:Label>
                                        <asp:TextBox runat="server" ID="AddQtyValue" text="1" CssClass="qty_input"></asp:TextBox>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="PartName">
                                <ItemTemplate>
                                    <div class="part-description">
                                        <asp:Label runat="server" Text='<%# Bind("PartName") %>' ID="PartNameLabel"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="UnitPrice">
                                <ItemTemplate>
                                    <div class="product-unit-price">
                                        <asp:Label runat="server" Text='<%# "$" + Eval("UnitPrice", "{0:0.00}") %>' ID="UnitPriceLabel"></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="QtyInStock">
                                <ItemTemplate>
                                    <div class="stock-amount">
                                        <asp:Label runat="server" ID="QtyInStockLabel"><span class="stock-span">Stock:</span> <%# Eval("QtyInStock") %></asp:Label>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </LoggedInTemplate>
            </asp:LoginView>
        </div>
    </div>
    <asp:ObjectDataSource ID="ProductListByCatODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="List_PartsByCategory" TypeName="eBike.System.BLL.Sales.PartsController">
        <SelectParameters>
            <asp:ControlParameter ControlID="CurrentCategoryId" PropertyName="Text" Name="catId" Type="Int32"></asp:ControlParameter>
            <asp:ControlParameter ControlID="CurrentLoggedInUser" PropertyName="Text" Name="username" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
