<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Purchasing.aspx.cs" Inherits="Pages_Purchasing" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="jumbotron">
        <h1>Purchasing</h1>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>

    <asp:Label ID="VendorLabel" runat="server" Text="Vendors"></asp:Label>
    <asp:DropDownList ID="VendorsDisplay" runat="server" DataSourceID="ObjectDataSource1" DataTextField="VendorName" DataValueField="VendorID" AppendDataBoundItems="true">
        <asp:ListItem Value="0">Select a Vendor</asp:ListItem>
    </asp:DropDownList>
    <asp:Button ID="SearchEvent" runat="server" Text="Search" OnClick="SearchEvent_Click" />
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" OldValuesParameterFormatString="original_{0}" SelectMethod="Get_Vendors" TypeName="eBike.System.BLL.VendorController"></asp:ObjectDataSource>


    <asp:GridView ID="VendorInformation" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
        <AlternatingRowStyle BorderColor="Green" BorderWidth="1px" BorderStyle="Dashed"></AlternatingRowStyle>
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Width="200px" />
        <EmptyDataRowStyle BackColor="Yellow" />
        <FooterStyle BackColor="#CCCC99" ForeColor="Black"></FooterStyle>

        <HeaderStyle BackColor="#333333" Font-Bold="True" Font-Underline="True" ForeColor="White" Width="200px"></HeaderStyle>

        <PagerStyle BackColor="White" Font-Underline="True" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle Width="200px"></RowStyle>

        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" BorderColor="Green" BorderStyle="Dashed" BorderWidth="1px" ForeColor="#284775" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" Font-Underline="True" ForeColor="White" Width="200px" />
        <Columns>
            <asp:TemplateField HeaderText="VendorID" SortExpression="VendorID">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("VendorID") %>' ID="Label1"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="VendorName" SortExpression="VendorName">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("VendorName") %>' ID="Label2"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="phone" SortExpression="phone"></asp:TemplateField>
            <asp:TemplateField HeaderText="address" SortExpression="address">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("address") %>' ID="Label4"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="city" SortExpression="city">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("city") %>' ID="Label5"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="province" SortExpression="province">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("province") %>' ID="Label6"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="postalcode" SortExpression="postalcode">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%# Bind("postalcode") %>' ID="Label7"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
    <asp:ObjectDataSource runat="server" ID="ObjectDataSource2" OldValuesParameterFormatString="original_{0}" SelectMethod="Get_VendorInformation" TypeName="eBike.System.BLL.VendorController">
        <SelectParameters>
            <asp:ControlParameter ControlID="VendorsDisplay" PropertyName="SelectedValue" Name="vendorid" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>


    <asp:ListView ID="PurchaseOrderView" runat="server" OnItemCommand="REMOVE">
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
       
        <ItemTemplate>
            <table>
            <tr style="background-color: #E0FFFF; color: #333333;">
                <td style="width:200px; text-align:center;">
                     <asp:Button runat="server" CommandName="Remove" Text="Remove" ID="RemoveButton" />
                 </td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("itemid") %>' runat="server" ID="itemid" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="Description" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("QOH") %>' runat="server" ID="QOH" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("ROL") %>' runat="server" ID="ROL" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("QOO") %>' runat="server" ID="QOO" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:TextBox Text='<%# Eval("Quantity") %>' runat="server" ID="Quantity" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:TextBox  Text='<%# Eval("Price") %>' runat="server" ID="Price" /></td>
            </tr>
                </table>
        </ItemTemplate>

        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF;">
                            <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                <th style="width:200px; text-align:center;" runat="server"></th>
                                <th style="width:200px; text-align:center;" runat="server">itemid</th>
                                <th style="width:200px; text-align:center;" runat="server">Description</th>
                                <th style="width:200px; text-align:center;" runat="server">QOH</th>
                                <th style="width:200px; text-align:center;" runat="server">ROL</th>
                                <th style="width:200px; text-align:center;" runat="server">QOO</th>
                                <th style="width:200px; text-align:center;" runat="server">Quantity</th>
                                <th style="width:200px; text-align:center;" runat="server">Price</th>
                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF"></td>
                </tr>
            </table>
        </LayoutTemplate>
    </asp:ListView>
    

    <asp:Label ID="SubTotallabel" runat="server" Text="SubTotal"></asp:Label>
    <asp:TextBox ID="SubTotal" runat="server"></asp:TextBox>

    <asp:Label ID="Taxlabel" runat="server" Text="Tax"></asp:Label>
    <asp:TextBox ID="Tax" runat="server"></asp:TextBox>

    <asp:Label ID="Amountlabel" runat="server" Text="Amount"></asp:Label>
    <asp:TextBox ID="Amount" runat="server"></asp:TextBox>

    <asp:Button ID="Update" runat="server" Text="Update" OnClick="Update_Click" />
    <asp:Button ID="Delete" runat="server" Text="Delete" OnClick="Delete_Click" />
    <asp:Button ID="Place" runat="server" Text="Place" OnClick="Place_Click" />
    <asp:Button ID="Clear" runat="server" Text="Clear" OnClick="Clear_Click" />

    <asp:ListView ID="CurrentInventoryView" runat="server" OnItemCommand="ADD">
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        
        <ItemTemplate>
            <tr style="background-color: #E0FFFF; color: #333333;">
                <td style="width:200px; text-align:center;">
                     <asp:Button runat="server" CommandName="Add" Text="Add" ID="AddButton" />
                </td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("itemid") %>' runat="server" ID="itemid" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("Description") %>' runat="server" ID="Description" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("QOH") %>' runat="server" ID="QOH" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("ROL") %>' runat="server" ID="ROL" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("QOO") %>' runat="server" ID="QOO" /></td>
                <td style="width:200px; text-align:center;">
                    <asp:Label Text='<%# Eval("Price") %>' runat="server" ID="Price" /></td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                            <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                <th style="width:200px; text-align:center;" runat="server"></th>
                                <th style="width:200px; text-align:center;" runat="server">itemid</th>
                                <th style="width:200px; text-align:center;" runat="server">Description</th>
                                <th style="width:200px; text-align:center;" runat="server">QOH</th>
                                <th style="width:200px; text-align:center;" runat="server">ROL</th>
                                <th style="width:200px; text-align:center;" runat="server">QOO</th>
                                <th style="width:200px; text-align:center;" runat="server">Price</th>
                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF"></td>
                </tr>
            </table>
        </LayoutTemplate>
        
    </asp:ListView>
</asp:Content>

