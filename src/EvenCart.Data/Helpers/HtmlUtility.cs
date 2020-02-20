using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace EvenCart.Data.Helpers
{
    public class HtmlUtility
    {
        /// <summary>
        /// Strips all html from the provided html content
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        public static string StripHtml(string htmlContent)
        {
            if (string.IsNullOrEmpty(htmlContent))
                return "";
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            return HttpUtility.HtmlDecode(doc.DocumentNode.InnerText);
        }

        /// <summary>
        /// Safely removes tags other than acceptable tags from the provided html content
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <param name="acceptableTags"></param>
        /// <returns></returns>
        public static string RemoveUnwantedTags(string htmlContent, string[] acceptableTags = null)
        {
            var document = new HtmlDocument();
            document.LoadHtml(htmlContent);

            acceptableTags = acceptableTags ?? new[] { "strong", "em", "u", "i", "a", "div" };

            var nodes = new Queue<HtmlNode>(document.DocumentNode.SelectNodes("./*|./text()"));
            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();
                var parentNode = node.ParentNode;

                if (!acceptableTags.Contains(node.Name) && node.Name != "#text")
                {
                    var childNodes = node.SelectNodes("./*|./text()");

                    if (childNodes != null)
                    {
                        foreach (var child in childNodes)
                        {
                            nodes.Enqueue(child);
                            parentNode.InsertBefore(child, node);
                        }
                    }

                    parentNode.RemoveChild(node);

                }
            }

            return document.DocumentNode.InnerHtml;
        }

        public static string GetRandomEmail()
        {
            var guestEmail = $"guest-{Guid.NewGuid():D}@localaccount.com";
            return guestEmail;
        }
    }
}