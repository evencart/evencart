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
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using EvenCart.Core.Infrastructure.Utils;
using EvenCart.Data.Extensions;

namespace EvenCart.Services.Seo
{
    public class SitemapGenerator : ISitemapGenerator
    {
        private const string Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9";

        public string GetSitemapXml()
        {
            var xmlDoc = new XmlDocument();
            //root node
            AppendRootNode(xmlDoc);

            var sitemapProviders = TypeFinder.ClassesOfType<ISitemapProvider>()
                .Select(x => (ISitemapProvider) Activator.CreateInstance(x));
            foreach (var provider in sitemapProviders)
            {
                try
                {
                    var urls = provider.GetUrls();
                    foreach (var url in urls)
                    {
                        if (!url.IsNullEmptyOrWhiteSpace())
                            AppendUrlNode(xmlDoc, url);
                    }
                }
                catch
                {
                    //continue;
                }
               
            }
#if DEBUG
            return FormatXml(xmlDoc.OuterXml);
#else
            return xmlDoc.OuterXml;
#endif
        }

        private void AppendRootNode(XmlDocument xmlDoc)
        {
            var docNode = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(docNode);
            
            
            var rootNode = xmlDoc.CreateElement("urlset");
            xmlDoc.AppendChild(rootNode);

            rootNode.SetAttribute("xmlns", Namespace);
        }

        private void AppendUrlNode(XmlDocument xmlDoc, string url)
        {
            var urlNode = xmlDoc.CreateElement("url");
            var locNode = xmlDoc.CreateElement("loc");
            var locValueNode = xmlDoc.CreateTextNode(url);
            locNode.AppendChild(locValueNode);
            
            urlNode.AppendChild(locNode);
            xmlDoc.DocumentElement.AppendChild(urlNode);
        }

        private string FormatXml(string xml)
        {
            try
            {
                var doc = XDocument.Parse(xml);
                return doc.Declaration.ToString() + Environment.NewLine + doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }
    }
}