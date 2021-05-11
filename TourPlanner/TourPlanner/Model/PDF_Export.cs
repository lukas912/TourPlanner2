using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Drawing;

namespace TourPlanner.Model
{
    class PDF_Export
    {
        public void exportTourReport(Tour tour, List<TourLog> tour_logs, string path)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // Create fonts
            XFont log_font = new XFont("Verdana", 10, XFontStyle.Regular);

            XFont head_font = new XFont("Verdana", 14, XFontStyle.Regular);

            XFont head_font2 = new XFont("Verdana", 22, XFontStyle.Bold);

            int y = 0;

            int x = 75;

            //Basic Tour Info

            gfx.DrawString("TOUR REPORT", head_font2, XBrushes.Black,
            new XRect(0, y, page.Width.Point, page.Height.Point), XStringFormats.TopCenter);
            y += 60;

            gfx.DrawString("Tour ID: " + tour.ID.ToString(), head_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Title: " + tour.Title, head_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Description: " + tour.Description, head_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 40;

            gfx.DrawString("Tour Logs: ", head_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 30;


            foreach (TourLog tl in tour_logs)
            {
                gfx.DrawString("Log ID: " + tl.TourLogID.ToString(), log_font, XBrushes.Black,
                new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                y += 20;

                gfx.DrawString("Timestamp: " + tl.Timestamp, log_font, XBrushes.Black,
                new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                y += 20;

                gfx.DrawString("Report: " + tl.Report, log_font, XBrushes.Black,
                new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                y += 20;

                gfx.DrawString("Distance: " + tl.Distance, log_font, XBrushes.Black,
                new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                y += 20;

                gfx.DrawString("Total Time: " + tl.TotalTime, log_font, XBrushes.Black,
                new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                y += 20;

                gfx.DrawString("Rating: " + tl.Rating, log_font, XBrushes.Black,
                new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
                y += 40;

            }

            document.Save(path);


        }
    }
}
