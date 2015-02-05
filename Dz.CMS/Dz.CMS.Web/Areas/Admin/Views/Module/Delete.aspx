<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/IFrame.Master" Inherits="System.Web.Mvc.ViewPage<Dz.CMS.Model.ModelBase>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>

    <h3>Are you sure you want to delete this?</h3>

    <% using (Html.BeginForm())
       { %>
    <fieldset>
        <legend>删除数据</legend>
        <%:Html.DisplayForModel() %>

    </fieldset>

    <p>
        <input type="submit" value="删除" />
    </p>
    <% } %>
    <%: Html.ActionLink("Back to List", "Index") %>
</asp:Content>
