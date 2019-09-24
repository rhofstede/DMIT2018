<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewCRUDODS.aspx.cs" Inherits="WebApp.SamplePages.ListViewCRUDODS" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ODS List View</h1>
    <blockquote class="alert alert-info">
        This page will demonstrate a CRUD process using the list view control and only ODS data sources.
    </blockquote>
    <br />
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <br />
    <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" HeaderText="Insert Record concerns. Please adjust as necessary." ValidationGroup="IGroup" />
    <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server" HeaderText="Edit Record concerns. Please adjust as necessary." ValidationGroup="EGroup" />

    <asp:ListView ID="AlbumList" runat="server" DataSourceID="AlbumListODS" DataKeyNames="AlbumID" InsertItemPosition="LastItem">

        <AlternatingItemTemplate>
            <tr style="background-color: #FFFFFF; color: #284775;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Remove" ID="DeleteButton" OnClientClick="return confirm('Are you sure you want to delete this record?')" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumID") %>' runat="server" ID="AlbumIDLabel" Width="50px" Enabled="false" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" Width="400px" /></td>
                <td>
                    <asp:DropDownList runat="server" ID="ArtistList" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" SelectedValue='<%# Eval("ArtistId") %>' Enabled="false" Width="300px" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" Width="50px" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>

            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <asp:RequiredFieldValidator ID="RequiredTitleTextBoxE" runat="server" ErrorMessage="Title is required." Display="None" ControlToValidate="TitleTextBoxE" ValidationGroup="EGroup"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegExTitleTextBoxE" runat="server" ErrorMessage="Title is limited to 160 characters." ControlToValidate="TitleTextBoxE" ValidationExpression="^.{1,160}$" ValidationGroup="EGroup" Display="None"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredReleaseYearTextBoxE" runat="server" ErrorMessage="Release year is required." Display="None" ControlToValidate="ReleaseYearTextBoxE" ValidationGroup="EGroup"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeReleaseYearTextBoxE" runat="server" ErrorMessage="Year must be between 1950 and today." Display="None" ControlToValidate="ReleaseYearTextBoxE" ValidationGroup="EGroup" MinimumValue="1950" MaximumValue='<%#DateTime.Today.Year%>' Type="Integer"></asp:RangeValidator>      
            <asp:RegularExpressionValidator ID="RegExReleaseYearTextBoxE" runat="server" ErrorMessage="Release label is limited to 50 characters." ControlToValidate="ReleaseLabelTextBoxE" ValidationExpression="^.{0, 50}$" ValidationGroup="EGroup" Display="None"></asp:RegularExpressionValidator>

            <tr style="background-color: hotpink;">
                <td>
                    <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" ValidationGroup="EGroup" />
                    <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" OnClientClick="return confirm('Are you sure you want to cancel?')" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumID") %>' runat="server" ID="AlbumIDTextBox" Width="50px" Enabled="false" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxE" Width="400px" /></td>
                <td>

                    <asp:DropDownList runat="server" ID="ArtistList" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" SelectedValue='<%# Bind("ArtistId") %>' Width="300px" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBoxE" Width="50px" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxE" /></td>

            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <asp:RequiredFieldValidator ID="RequiredTitleTextBoxI" runat="server" ErrorMessage="Title is required." Display="None" ControlToValidate="TitleTextBoxI" ValidationGroup="IGroup"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegExTitleTextBoxI" runat="server" ErrorMessage="Title is limited to 160 characters." ControlToValidate="TitleTextBoxI" ValidationExpression="^.{1,160}$" ValidationGroup="IGroup" Display="None" ></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredReleaseYearTextBoxI" runat="server" ErrorMessage="Release year is required." Display="None" ControlToValidate="ReleaseYearTextBoxI" ValidationGroup="IGroup"></asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeReleaseYearTextBoxI" runat="server" ErrorMessage="Year must be between 1950 and today." Display="None" ControlToValidate="ReleaseYearTextBoxI" ValidationGroup="IGroup" MinimumValue="1950" MaximumValue='<%# DateTime.Today.Year %>' Type="Integer"></asp:RangeValidator>
            <asp:RegularExpressionValidator ID="RegExReleaseLabelTextBoxI" runat="server" ErrorMessage="Release label is limited to 50 characters." ControlToValidate="ReleaseLabelTextBoxI" ValidationExpression="^.{0, 50}$" ValidationGroup="IGroup" Display="None"></asp:RegularExpressionValidator>
            <tr style="background-color: rebeccapurple; color: hotpink">
                <td>
                    <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" ValidationGroup="IGroup" />
                    <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" OnClientClick="return confirm('Are you sure you want to cancel?')" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumID") %>' runat="server" ID="AlbumIDTextBox" Width="50px" Enabled="false" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxI" Width="400px" /></td>
                <td>
                    <asp:DropDownList runat="server" ID="ArtistList" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" SelectedValue='<%# Bind("ArtistId") %>' Width="300px" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBoxI" Width="50px" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxI" /></td>

            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #E0FFFF; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Delete" Text="Remove" ID="DeleteButton" OnClientClick="return confirm('Are you sure you want to delete this record?')" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumID") %>' runat="server" ID="AlbumIDLabel" Width="50px" Enabled="false" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" Width="400px" /></td>
                <td>
                    <asp:DropDownList runat="server" ID="ArtistList" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" SelectedValue='<%# Eval("ArtistId") %>' Enabled="false" Width="300px" />

                </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" Width="50px" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>

            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                            <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                <th runat="server"></th>
                                <th runat="server">ID</th>
                                <th runat="server">Title</th>
                                <th runat="server">Artist</th>
                                <th runat="server">Year</th>
                                <th runat="server">Label</th>

                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: midnightblue">
                        <asp:DataPager runat="server" ID="DataPager1">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="true" ShowPreviousPageButton="true"></asp:NextPreviousPagerField>
                                <asp:NumericPagerField></asp:NumericPagerField>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                <td>
                    <asp:Button runat="server" CommandName="Remove" Text="Delete" ID="DeleteButton" OnClientClick="return confirm('Are you sure you want to delete this record?')" />
                    <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumID") %>' runat="server" ID="AlbumIDLabel" Width="50px" Enabled="false" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" Width="400px" /></td>
                <td>
                    <asp:DropDownList runat="server" ID="ArtistList" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" SelectedValue='<%# Eval("ArtistId") %>' Enabled="false" Width="300px" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" Width="50px" /></td>
                <td>
                    <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Artist") %>' runat="server" ID="ArtistLabel" /></td>
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>

    <asp:ObjectDataSource ID="AlbumListODS" runat="server" DataObjectTypeName="ChinookSystem.Data.Entities.Album" SelectMethod="Album_List" InsertMethod="Album_Add" UpdateMethod="Album_Update" DeleteMethod="Album_Delete" OnDeleted="CheckForException" OnInserted="CheckForException" OnUpdated="CheckForException" OnSelected="CheckForException" OldValuesParameterFormatString="original_{0}" TypeName="ChinookSystem.BLL.AlbumController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Artist_List" TypeName="ChinookSystem.BLL.ArtistController" OnSelected="CheckForException"></asp:ObjectDataSource>
</asp:Content>
