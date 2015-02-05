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
                <%: Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
               <%: Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }) %> |
              <%: Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }) %>
            </td>
        </tr>
        <% } %>
    </table>

</asp:Content>
