using System.Collections.Specialized;
using HtmlAgilityPack;

namespace EvenCart.Infrastructure.MediaServices
{
    public interface IHtmlProcessor
    {
        HtmlNode GetUrl(string url);

        HtmlNode GetBodyContent(string url);

        string GetContentByXPath(string html, string xPath, bool textOnly = false);

        HtmlDocument LoadHtml(string html);

        string GetContentByXPath(HtmlDocument document, string xPath, bool textOnly = false);

        string Post(string url, NameValueCollection data);

        HtmlNodeCollection GetNodes(HtmlNode bodyNode, string xPath);

        byte[] GetBytes(string url);
    }
}