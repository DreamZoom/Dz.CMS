﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/IFrame.Master" Inherits="System.Web.Mvc.ViewPage<Dz.CMS.Model.ModelBase>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Add
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>填写数据</legend>

        <%:Html.EditorForModel() %>

        <p>
            <input type="submit" value="创建" />
        </p>
    </fieldset>
<% } %>

<div>
    <%: Html.ActionLink("回到列表", "Index", new { serviceName= Request.Params["serviceName"]})%>
</div>

</asp:Content>
