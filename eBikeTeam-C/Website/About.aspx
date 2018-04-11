<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page.</h3>
    <div class="row">
        <div class="col-sm-4">
            <h3>Team Members:</h3>
            <h4>Barrett Long</h4>
            <h4>Mike Bublitz</h4>
            <h4>Baldwin Kah</h4>
            <h4>Parniesh Padam</h4>
        </div>
        <div class="col-sm-4">
            <h3>Planned Security Roles</h3>
            <ul>
                <li>Webmaster</li>
                <li>Admin</li>
                <li>Staff</li>
                <li>RegisteredUser</li>
            </ul>
        </div>
        <div class="col-sm-4">
            <h3>Users | Passwords</h3>
            <ul>
                <li><strong>webmaster</strong> - Pa$$wordTeam3</li>
                <li><strong>testAdmin</strong> - Pa$$wordTeam3</li>
                <li><strong>testEmployee</strong> - Pa$$wordTeam3</li>
                <li><strong>testCustomer</strong> - Pa$$wordTeam3</li>
                <li><strong>blong</strong> - Pa$$wordLONG</li>
                <li><strong>bkah</strong> - Pa$$wordKAH</li>
                <li><strong>ppadam</strong> - Pa$$wordPADAM</li>
                <li><strong>mbublitz</strong> - Pa$$wordBUBLITZ</li>
            </ul>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-sm-12">
            <h3>Development Connection String</h3>
            <p>We will be using the following connection string during this project.</p>
            <code>
                	&lt;connectionStrings&gt; <br />
                    	&lt;add name="eBikesDB" connectionString="Data Source=.; Initial Catalog=eBikes; Integrated Security=true" providerName ="System.Data.SqlClient" /&gt;<br />
                    	&lt;add name="DefaultConnection" connectionString="Data Source=.; Initial Catalog=eBikes; Integrated Security=SSPI" providerName="System.Data.SqlClient" /&gt;<br />
                	&lt;/connectionStrings&gt;
            </code>
        </div>
    </div>
</asp:Content>
