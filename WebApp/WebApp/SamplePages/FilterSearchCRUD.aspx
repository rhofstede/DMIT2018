<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FilterSearchCRUD.aspx.cs" Inherits="WebApp.SamplePages.FilterSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Review Basic Crud</h1>

    <div class="row">
        <div class="col-sm-offset-3">
            <asp:Label ID="Label1" runat="server" Text="Select an artist to view albums:"></asp:Label>&nbsp&nbsp&nbsp&nbsp
            <asp:DropDownList ID="ArtistList" runat="server"></asp:DropDownList>
        </div>
    </div>
</asp:Content>
