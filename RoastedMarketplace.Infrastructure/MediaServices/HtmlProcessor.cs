using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using HtmlAgilityPack;

namespace RoastedMarketplace.Infrastructure.MediaServices
{
    public class HtmlProcessor : IHtmlProcessor
    {
        public HtmlNode GetUrl(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var web = new HtmlWeb();

            var document = web.Load(url);
            if (web.StatusCode != HttpStatusCode.OK)
                return null;

            return document.DocumentNode;
        }

        public HtmlNode GetBodyContent(string url)
        {
            return GetUrl(url)?.SelectSingleNode(@"/html/body");
        }

        public string GetContentByXPath(string html, string xPath, bool textOnly = false)
        {
            try
            {
                var doc = LoadHtml(html);
                return GetContentByXPath(doc, xPath, textOnly);
            }
            catch
            {
                return null;
            }

        }

        public HtmlDocument LoadHtml(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }

        public string GetContentByXPath(HtmlDocument document, string xPath, bool textOnly = false)
        {
            var node = document.DocumentNode.SelectSingleNode(xPath);
            if (node == null)
                return null;
            if (node.Name == "input")
                return node.GetAttributeValue("value", "");
            return textOnly
                ? node.InnerText
                : node.InnerHtml;
        }

        public string Post(string url, NameValueCollection data)
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    var responseBytes = webClient.UploadValues(url, "POST", data);
                    var result = Encoding.UTF8.GetString(responseBytes);
                    return result;
                }
                catch (Exception e)
                {
                    return "";
                }
            }
        }

        public HtmlNodeCollection GetNodes(HtmlNode bodyNode, string xPath)
        {
            try
            {
                return bodyNode.SelectNodes(xPath);
            }
            catch
            {
                return null;
            }
        }

        public byte[] GetBytes(string url)
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    var responseBytes = webClient.DownloadData(url);
                    return responseBytes;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}