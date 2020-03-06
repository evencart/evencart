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

using DinkToPdf;
using DinkToPdf.Contracts;

namespace EvenCart.Services.Pdf
{
    public class PdfService : IPdfService
    {
        private readonly IConverter _converter;
        public PdfService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GetPdfBytes(string html)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4Plus,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "[page]/[toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };
            return _converter.Convert(doc);
        }
    }
}