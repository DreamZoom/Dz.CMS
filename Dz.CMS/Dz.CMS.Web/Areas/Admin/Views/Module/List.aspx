<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/IFrame.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Dz.CMS.Model.ModelBase>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <table>
        <tr>

            <%:Html.TableHeader(Model.FirstOrDefault()) %>
            <th>操作</th>
        </tr>

        <% foreach (var item in Model)
           { %>
        <tr>
            <%:Html.TableRow(item) %>
            <td>
                <%: Html.ActionLink("编辑", "Edit", new { id=item.GetFiledValue("ID"),serviceName= Request.Params["serviceName"]  }) %> |
                <%: Html.ActionLink("详情", "Details", new { id=item.GetFiledValue("ID"),serviceName= Request.Params["serviceName"] }) %> |
                <%: Html.ActionLink("删除", "Delete", new { id=item.GetFiledValue("ID"),serviceName= Request.Params["serviceName"]}) %>
            </td>
        </tr>
        <% } %>
    </table>
    <div>
         <%: Html.ActionLink("创建", "Add", new { serviceName= Request.Params["serviceName"]  }) %> 
    </div>
</asp:Content>
