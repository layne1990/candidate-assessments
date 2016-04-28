using MarkdownSharp;
using Microsoft.AspNet.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CandidateAssessments.Helpers
{
    public static class MarkdownHelper
    {

        static Markdown markdownTransformer = new Markdown();

        public static IHtmlString Markdown(this HtmlHelper helper, string text)
        {
            string html = markdownTransformer.Transform(text);

            return new HtmlString(html);
        }

    }
}
