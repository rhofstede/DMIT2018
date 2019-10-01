<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepeaterExample.aspx.cs" Inherits="WebApp.SamplePages.RepeaterExample" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Nested LINQ Query with Repeater</h1>
    <%--header at top, item shows single dto record, alternating item template shows every other dto record, footer at bottom--%>
    <asp:Repeater ID="AlbumTracksList" runat="server" DataSourceID="AlbumTracksListODS" ItemType="ChinookSystem.Data.DTOs.ArtistAlbum">
        <HeaderTemplate>
            <h3 >Albums and Tracks</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <h5><strong>Album: <%# Item.AlbumTitle %></strong></h5>
            <p><strong>Artist: <%# Item.ArtistName %></strong> Track Count: <%# Item.TrackCount %> Play Time: <%# Item.AlbumPlayTime %></p>
            <asp:GridView ID="TrackList" runat="server" DataSource="<%# Item.AlbumTracks %>" CssClass="table" GridLines="Horizontal" BorderStyle="none"><%--don't use another ods. data comes from poco inside dto--%>                
            </asp:GridView>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <h5><strong>Album: <%# Item.AlbumTitle %></strong></h5>
            <p><strong>Artist: <%# Item.ArtistName %></strong> Track Count: <%# Item.TrackCount %> Play Time: <%# Item.AlbumPlayTime %></p>
            <asp:GridView ID="TrackList" runat="server" DataSource="<%# Item.AlbumTracks %>" CssClass="table" GridLines="Horizontal" BorderStyle="Solid">
            </asp:GridView>
        </AlternatingItemTemplate>
        <FooterTemplate>
            &copy; DMIT2018 NAIT All rights reserved
        </FooterTemplate>
    </asp:Repeater> <%--always add the item type by pointing to the class--%>

    <asp:ObjectDataSource ID="AlbumTracksListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Album_ArtistAlbum" TypeName="ChinookSystem.BLL.AlbumController"></asp:ObjectDataSource>
</asp:Content>
