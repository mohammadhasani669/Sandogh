using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;


namespace Sandogh.Common
{
    [HtmlTargetElement("div", Attributes = "page-model")]

    public class PagerTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;
        public PagerTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }


        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public string PageSearch { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new("div");

            PreButton(urlHelper, result);

            foreach (var i in PageModel.PageCount)
            {
                TagBuilder tag = new("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageSize = PageModel.PageSize, search = PageSearch, pageNumber = i });

                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.PageNumber
                    ? PageClassSelected : PageClassNormal);
                }


                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }
            output.Content.AppendHtml(result.InnerHtml);
        }

        private void PreButton(IUrlHelper urlHelper, TagBuilder result)
        {
            TagBuilder tagPre = new("a");
            tagPre.Attributes["href"] = urlHelper.Action(PageAction, new { pageSize = PageModel.PageSize, search = PageSearch, pageNumber = PageModel.PageNumber - 1 });
            tagPre.AddCssClass(PageModel.PageNumber > 1 ? PageClass : "disabled");
            tagPre.AddCssClass(PageClassNormal);
            tagPre.InnerHtml.Append("قبلی");
            result.InnerHtml.AppendHtml(tagPre);
        }
    }
}
