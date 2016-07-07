using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Encodings.Web;

namespace CandidateAssessments.TagHelpers
{
    [HtmlTargetElement("pager", Attributes = "total-pages, current-page, link-url, additional-parameters")]
    public class PagerTagHelper : TagHelper
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        [HtmlAttributeName("link-url")]
        public string Url { get; set; }
        public string AdditionalParameters { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "div";
            output.PreContent.SetHtmlContent("<ul class=\"pagination pagination-lg\">"); 
            

            var items = new StringBuilder();
            for (var i = 1; i <= TotalPages; i++)
            {
                var li = new TagBuilder("li");

                if (i == CurrentPage)
                {
                    li.AddCssClass("active");
                }

                var a = new TagBuilder("a");
                a.MergeAttribute("href", $"{Url}?page={i}&{AdditionalParameters}");
                a.MergeAttribute("title", $"Click to go to page {i}");
                a.InnerHtml.AppendHtml(i.ToString());
                
                li.InnerHtml.AppendHtml(a);


                var writer = new System.IO.StringWriter();
                li.WriteTo(writer, HtmlEncoder.Default);
                var s = writer.ToString();
                items.AppendLine(s);
            }
            output.Content.SetHtmlContent(items.ToString());
            output.PostContent.SetHtmlContent("</ul>"); 
            output.Attributes.Clear();
            output.Attributes.Add("class", "pager");
        }
    }
}