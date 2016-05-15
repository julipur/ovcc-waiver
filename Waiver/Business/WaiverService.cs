using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Web.Mvc;
using System.Drawing;
using PdfSharp.Fonts;

namespace Waiver.Business
{
    public class WaiverService
    {
        private readonly string _waiverFilePath;
        private readonly string _newFile;
        public WaiverService(string oldFile, string newFile)
        {
            _waiverFilePath = oldFile;
            _newFile = newFile;
        }

        public void GenerateWaiver(string firstName, string lastName, string guardian, string dob, string phoneNumber, string club, string image)
        {
            PdfDocument waiverDocument = PdfReader.Open(_waiverFilePath, PdfDocumentOpenMode.Import);
            PdfDocument signedWaiver = new PdfDocument();

            // page 1
            PdfPage page1 = signedWaiver.AddPage(waiverDocument.Pages[0]);

            double fontHeight = 20;
            XRect firstNameRectangle = new XRect(82, 250, 200, fontHeight);
            XFont font = new XFont("Arial", 15, XFontStyle.Regular);

            XGraphics page1Graphics = XGraphics.FromPdfPage(page1);
            page1Graphics.DrawString(firstName, font, XBrushes.Black, new XRect(82, 250, 200, fontHeight), XStringFormats.BottomCenter);
            page1Graphics.DrawString(lastName, font, XBrushes.Black, new XRect(325, 250, 200, fontHeight), XStringFormats.BottomCenter);
            page1Graphics.DrawString(dob, font, XBrushes.Black, new XRect(90, 275, 200, fontHeight), XStringFormats.BottomCenter);
            page1Graphics.DrawString(phoneNumber, font, XBrushes.Black, new XRect(350, 275, 200, fontHeight), XStringFormats.BottomCenter);
            page1Graphics.DrawString(club, font, XBrushes.Black, new XRect(82, 300, 400, fontHeight), XStringFormats.BottomCenter);
            if (string.IsNullOrEmpty(guardian))
            {
                page1Graphics.DrawString(string.Format("{0} {1}", firstName, lastName), new XFont("Arial", 12, XFontStyle.Regular), XBrushes.Black, new XRect(15, page1.Height - 100, 400, 12), XStringFormats.BottomCenter);
            }
            // page 1
            PdfPage page2 = signedWaiver.AddPage(waiverDocument.Pages[1]);
            XGraphics page2Graphics = XGraphics.FromPdfPage(page2);

            if (string.IsNullOrEmpty(guardian))
            {
                page2Graphics.DrawImage(XImage.FromFile(image), new XRect(115, 300, 250, 50));
                page2Graphics.DrawString(DateTime.Now.ToString("MMM dd, yyyy"), font, XBrushes.Black, new XRect(350, 325, 200, fontHeight), XStringFormats.BottomCenter);
            }
            else 
            {
                page2Graphics.DrawString(guardian, new XFont("Arial", 12, XFontStyle.Regular), XBrushes.Black, new XRect(15, page1.Height - 260, 400, 12), XStringFormats.BottomCenter);
                page2Graphics.DrawImage(XImage.FromFile(image), new XRect(115, 690, 250, 50));
                page2Graphics.DrawString(DateTime.Now.ToString("MMM dd, yyyy"), font, XBrushes.Black, new XRect(355, 705, 200, fontHeight), XStringFormats.BottomCenter);
            }


            signedWaiver.Save(_newFile);
        }

    }
}