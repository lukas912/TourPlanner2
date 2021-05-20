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
using TourPlanner_Model;

namespace TourPlanner_Business
{
    public class PDF_Export
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

            int y = 40;

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
            y += 20;

            gfx.DrawString("Total Distance: " + tour.TotalDistance, head_font, XBrushes.Black,
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


            // Create an empty page
            PdfPage page2 = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx2 = XGraphics.FromPdfPage(page2);

            DrawImage(gfx2, tour.Image, 50, 50);

            document.Save(path);


        }

        void DrawImage(XGraphics gfx, string jpegSamplePath, int x, int y)
        {
            XImage image = XImage.FromFile(jpegSamplePath);
            gfx.DrawImage(image, x, y, 450, 300);
        }

        public void exportTourSummarizeReport(Tour tour, List<TourLog> tour_logs, string path)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            // Create fonts
            XFont log_font = new XFont("Verdana", 12, XFontStyle.Regular);

            XFont head_font2 = new XFont("Verdana", 22, XFontStyle.Bold);

            int y = 40;

            int x = 75;

            //Basic Tour Info

            gfx.DrawString("TOUR SUMMARIZE REPORT", head_font2, XBrushes.Black,
            new XRect(0, y, page.Width.Point, page.Height.Point), XStringFormats.TopCenter);
            y += 60;

            TourStats ts = getTourStats(tour, tour_logs);

            gfx.DrawString("Average Distance: " + ts.AVG_Distance.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Average Time: " + ts.AVG_Time.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Average Rating: " + ts.AVG_Rating.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Average Difficulty: " + ts.AVG_Difficulty.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Average Participants: " + ts.AVG_Participants.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 40;

            gfx.DrawString("Minumum Distance: " + ts.MIN_Distance.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Minimum Time: " + ts.MIN_Time.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Worst Rating: " + ts.MAX_Rating.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Minimum Difficulty: " + ts.MIN_Difficulty.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Minimum Participants: " + ts.MIN_Participants.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 40;

            gfx.DrawString("Maximum Distance: " + ts.MAX_Distance.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Maximum Time: " + ts.MAX_Time.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Best Rating: " + ts.MIN_Rating.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Maximum Difficulty: " + ts.MAX_Difficulty.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 20;

            gfx.DrawString("Maximum Participants: " + ts.MAX_Participants.ToString(), log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);
            y += 40;

            gfx.DrawString("Reccomendation Rate: " + (ts.RecommendationRate * 100).ToString() + "%", log_font, XBrushes.Black,
            new XRect(x, y, page.Width.Point, page.Height.Point), XStringFormats.TopLeft);

            document.Save(path);


        }

        private TourStats getTourStats(Tour tour, List<TourLog> tour_logs)
        {
            float ar = 0;
            float ds = 0;
            float avg_df = 0;
            float avg_pa = 0;

            float minRating = -1;
            float minDistance = -1;
            int minParticipants = -1;
            int minDifficulty = -1;

            float maxRating = -1;
            float maxDistance = -1;
            int maxParticipants = -1;
            int maxDifficulty = -1;

            int rc_true = 0;
            int rc_false = 0;



            foreach (TourLog tl in tour_logs)
            {
                ar += tl.Rating;
                ds += tl.Distance;
                avg_df += tl.Difficulty;
                avg_pa += tl.Participants;

                if(minRating == -1 || minRating > tl.Rating)
                {
                    minRating = tl.Rating;
                }

                if (minDistance == -1 || minDistance > tl.Distance)
                {
                    minDistance = tl.Distance;
                }

                if (minDifficulty == -1 || minDifficulty > tl.Difficulty)
                {
                    minDifficulty = tl.Difficulty;
                }

                if (minParticipants == -1 || minParticipants > tl.Participants)
                {
                    minParticipants = tl.Participants;
                }

                if (maxRating == -1 || maxRating < tl.Rating)
                {
                    maxRating = tl.Rating;
                }

                if (maxDistance == -1 || maxDistance < tl.Distance)
                {
                    maxDistance = tl.Distance;
                }

                if (maxDifficulty == -1 || maxDifficulty < tl.Difficulty)
                {
                    maxDifficulty = tl.Difficulty;
                }

                if (maxParticipants == -1 || maxParticipants < tl.Participants)
                {
                    maxParticipants = tl.Participants;
                }

                if(tl.Recommendation == true)
                {
                    rc_true++;
                }

                if (tl.Recommendation == false)
                {
                    rc_false++;
                }
            }

            ar = ar / tour_logs.Count;
            ds = ds / tour_logs.Count;
            avg_df = avg_df / tour_logs.Count;
            avg_pa = avg_pa / tour_logs.Count;

            TourStats ts = new TourStats(tour.ID, ds, "Not implemented", ar, minDistance, "Not implemented", minRating, maxDistance, "Not implemented", maxRating, minDifficulty, maxDifficulty, avg_df, minParticipants, maxParticipants, avg_pa, rc_true, rc_false);
            return ts;
        }
    }
}
