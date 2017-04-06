using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZZZ.ShoppingManager.UI.Models
{
    public static class PagenationHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,PagenationText page,Func<string,string> url)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");
            foreach (var item in page.GetNumberText())
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.SetInnerText(item);
                if(item!=page.SkitText)
                {
                    a.MergeAttribute("href", url(item));
                }
                li.InnerHtml += a;
                ul.InnerHtml += li;
            }
            return MvcHtmlString.Create(ul.ToString());
        }
    }
}