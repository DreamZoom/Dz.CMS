<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/IFrame.Master" Inherits="System.Web.Mvc.ViewPage<Dz.CMS.Model.ModelBase>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Edit</h2>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>编辑数据</legend>
        <%:Html.EditorForModel() %>
        <p>
            <input type="submit" value="保存" />
        </p>
    </fieldset>
<% } %>

<div>
   <%: Html.ActionLink("回到列表", "Index", new { serviceName= Request.Params["serviceName"]})%>
</div>

</asp:Content>
