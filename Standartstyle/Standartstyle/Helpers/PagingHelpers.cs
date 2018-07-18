using Standartstyle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Standartstyle.Helpers
{
    public static class PagingHelpers
    {
        private static int MAX_LEFT_LINKS = 4;
        private static int MAX_RIGHT_LINKS = 4;
        private static int MIDDLE_LINK_SIDES_COUNT = 2;
        private static int INDEX_OF_MIDDLE_LINK = 3;

        private static string leftArrowContent = "<span class='oi oi-arrow-thick-left'></span>";
        private static string rightArrowContent = "<span class='oi oi-arrow-thick-right'></span>";

        public static MvcHtmlString PageLinks(this HtmlHelper html, PageInfoModel pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();

            int firstPage = 0, upperPageLimit = 0;

            firstPage = pageInfo.CurrentPageNumber > INDEX_OF_MIDDLE_LINK ? pageInfo.CurrentPageNumber - MIDDLE_LINK_SIDES_COUNT : 1;
            if (pageInfo.TotalPages - MAX_LEFT_LINKS < firstPage && pageInfo.TotalPages >= 1 + MAX_LEFT_LINKS)
            {
                firstPage = pageInfo.TotalPages - MAX_LEFT_LINKS;
            }
            upperPageLimit = (firstPage + MAX_RIGHT_LINKS) > pageInfo.TotalPages ? pageInfo.TotalPages : firstPage + MAX_RIGHT_LINKS;

            TagBuilder left = createArrowNavigation(firstPage, pageInfo, upperPageLimit, pageUrl, true);
            TagBuilder right = createArrowNavigation(firstPage, pageInfo, upperPageLimit, pageUrl, false);

            result.Append(left.ToString());
            for (var i = firstPage; i <= upperPageLimit; i++)
            {
                TagBuilder listElement = createListElement();
                TagBuilder link = createLink();
                link.MergeAttribute("href", pageUrl(i));
                link.InnerHtml = i.ToString();
                if (i == pageInfo.CurrentPageNumber)
                {
                    listElement.AddCssClass("active");
                }
                listElement.InnerHtml = link.ToString();
                result.Append(listElement.ToString());
            }
            result.Append(right.ToString());

            return MvcHtmlString.Create(result.ToString());
        }

        private static TagBuilder createLink()
        {
            TagBuilder link = new TagBuilder("a");
            link.AddCssClass("page-link");
            return link;
        }

        private static TagBuilder createListElement()
        {
            TagBuilder listElement = new TagBuilder("li");
            listElement.AddCssClass("page-item");
            return listElement;
        }

        private static TagBuilder createArrowNavigation(int firstPage, PageInfoModel pageInfo, int upperPageLimit, Func<int, string> pageUrl, Boolean isLeftArrow)
        {
            TagBuilder arrow = createListElement();
            TagBuilder linkForArrow = createLink();
            if (isLeftArrow)
            {
                linkForArrow.InnerHtml = leftArrowContent;
                if (firstPage == pageInfo.CurrentPageNumber)
                {
                    arrow.AddCssClass("disabled");
                }
                else
                {
                    linkForArrow.MergeAttribute("href", pageUrl(pageInfo.CurrentPageNumber - 1));
                }
            }
            else
            {
                linkForArrow.InnerHtml = rightArrowContent;
                if (upperPageLimit == pageInfo.CurrentPageNumber)
                {
                    arrow.AddCssClass("disabled");
                }
                else
                {
                    linkForArrow.MergeAttribute("href", pageUrl(pageInfo.CurrentPageNumber + 1));
                }
            }
            arrow.InnerHtml = linkForArrow.ToString();
            return arrow;
        }


    }
}