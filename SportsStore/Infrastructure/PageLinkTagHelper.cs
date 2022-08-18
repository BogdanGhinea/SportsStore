using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Infrastructure
{
    //mussen wir es in einem div elemen setzen mit de page -model name
    [HtmlTargetElement("div",Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

    public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            //der href(url) zu 1 2 3 seiten in Index
            urlHelperFactory = helperFactory;

        }
        [ViewContext]               //unter View Context kann man html manipulieren
        [HtmlAttributeNotBound]
        //zeigt uns auch das view context
        public ViewContext ViewContext { get; set; }
        //wie seht aus(PageInfo)
        public PagingInfo PageModel { get; set; }
        //wo soll er gehen (page-action)
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
           //wir haben html page in div  
            TagBuilder result = new TagBuilder("div");
            //wir werden  a(ancor)haben  für jedes Page
            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction,
                    new { productPage = i });

                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    if (i == PageModel.CurrentPage)
                    {
                        tag.AddCssClass(PageClassSelected);
                    }
                    else
                    {
                        tag.AddCssClass(PageClassNormal);
                    }
                }

                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}
