using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReviewApp.Models;
using System.Text.Encodings.Web;

namespace ReviewApp.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent ReviewDisplay(this IHtmlHelper htmlHelper, Review review)
        {
            if (review == null)
            {
                return HtmlString.Empty;
            }

            var divTag = new TagBuilder("div");
            divTag.AddCssClass("review-item"); // Опциональный CSS-класс

            var authorTag = new TagBuilder("h3");
            authorTag.InnerHtml.Append($"{review.AuthorName} ({review.Rating}/5)");
            divTag.InnerHtml.AppendHtml(authorTag);

            var textTag = new TagBuilder("p");
            textTag.InnerHtml.Append(review.ReviewText);
            divTag.InnerHtml.AppendHtml(textTag);

            var smallTag = new TagBuilder("small");
            smallTag.InnerHtml.Append($"{review.CreatedDate.ToShortDateString()} {review.CreatedDate.ToShortTimeString()}");
            divTag.InnerHtml.AppendHtml(smallTag);

            var hrTag = new TagBuilder("hr"); // Добавляем горизонтальную линию после каждого отзыва
            divTag.InnerHtml.AppendHtml(hrTag);

            return divTag;
        }
    }
}
