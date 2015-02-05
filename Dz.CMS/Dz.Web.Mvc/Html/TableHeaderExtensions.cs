using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class TableHeaderExtensions
    {
        public static MvcHtmlString TableHeader(this HtmlHelper htmlHelper, object model)
        {
            StringBuilder sb=new StringBuilder();
            foreach(var p in model.GetType().GetProperties()){

                sb.AppendLine("<th>");
                sb.AppendLine(GetModelMetadata(model.GetType(),p.Name).DisplayName??p.Name);
                sb.AppendLine("</th>");
            }
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString TableRow(this HtmlHelper htmlHelper, object model)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var p in model.GetType().GetProperties())
            {

                sb.AppendLine("<td>");
                sb.AppendLine(p.GetValue(model).ToString());
                sb.AppendLine("</td>");
            }
            return new MvcHtmlString(sb.ToString());
        }

        public static ModelMetadata GetModelMetadata(Type type,string propertyName)
        {
            ModelMetadataProvider provider = ModelMetadataProviders.Current;
            ModelMetadata containerMetadata = new ModelMetadata(provider, null, () => null, type, null);
            return containerMetadata.Properties.FirstOrDefault(m => m.PropertyName == propertyName);
        }
    }
}
