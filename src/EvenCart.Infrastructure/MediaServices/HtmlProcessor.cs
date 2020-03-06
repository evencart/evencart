#region License
// Copyright (c) Sojatia Infocrafts Private Limited.
// The following code is part of EvenCart eCommerce Software (https://evencart.co) Dual Licensed under the terms of
// 
// 1. GNU GPLv3 with additional terms (available to read at https://evencart.co/license)
// 2. EvenCart Proprietary License (available to read at https://evencart.co/license/commercial-license).
// 
// You can select one of the above two licenses according to your requirements. The usage of this code is
// subject to the terms of the license chosen by you.
#endregion

using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace EvenCart.Infrastructure.MediaServices
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