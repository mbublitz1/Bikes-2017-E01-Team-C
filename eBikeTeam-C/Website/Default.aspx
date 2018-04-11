<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="text-align: center;">
        <h1>Team Name: P=MB²</h1>
        <img src="../img/team_Logo-bk.svg" alt="Large Team Logo" width="300" style="margin-bottom: 25px;"/>
        <p><strong>DMIT2018 - Final Project | Bikes R Us® Inc.</strong></p>
    </div>
    <div class="row">
        <div class="col-md-3">
            <h2>Team Member: <strong>Barrett Long</strong></h2>
            <p>Barrett is responsible for Sales!</p>    
        </div>
        <div class="col-md-3">
            <h2>Team Member: <strong>Mike Bublitz</strong></h2>
            <p>Mike is responsible for Receiving!</p> 
        </div>
        <div class="col-md-3">
            <h2>Team Member: <strong>Baldwin Kah</strong></h2>
            <p>Baldwin is responsible for Jobing!</p>        
        </div>
        <div class="col-md-3">
            <h2>Team Member: <strong>Parniesh Padam</strong></h2>
            <p>Parniesh is responsible for Purchasing!</p>
        </div>    
    </div>
    <div class="row">
       <div class="col-md-12">
            <h2>Bug Reporting</h2>
            <p>
                <i>This seciton will be used for reporting bugs in the future. Currently the only bug that is showing is the <strong>Roslyn problem.</strong></i>
                <i>As this is a know bug when using Github not really sure how to stop this from happeneing.</i>
            </p>
        </div>
    </div>
</asp:Content>
