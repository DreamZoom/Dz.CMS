<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/IFrame.Master" Inherits="System.Web.Mvc.ViewPage<Dz.CMS.Model.ModelBase>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>查看详情</legend>
    <%:Html.DisplayForModel() %>
</fieldset>
<p>
    <%: Html.ActionLink("编辑", "Edit", new { id=Model.GetFiledValue("ID"),serviceName= Request.Params["serviceName"]  }) %>
   
    <%: Html.ActionLink("回到列表", "Index", new { serviceName= Request.Params["serviceName"]})%>
</p>

</asp:Content>
