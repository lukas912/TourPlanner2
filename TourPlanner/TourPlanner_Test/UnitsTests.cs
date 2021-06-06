using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Model;
using TourPlanner_Business;
using System.IO;
using TourPlanner_ViewModel;

namespace TourPlanner_Test
{
    [TestFixture]
    class UnitsTests
    {
        //Suchfunktionen

        [Test]
        public void tourSearchTest()
        {
            Tour t1 = new Tour(0, "Tour 1", "Beschreibung", "img", "Wien", "Bregenz", 1.0f);
            Tour t2 = new Tour(0, "Tour 2", "Beschreibung", "img", "Wien", "Graz", 1.0f);
            List<Tour> tours = new List<Tour>();
            tours.Add(t1);
            tours.Add(t2);
            Assert.AreEqual(TourSearch.searchTours(tours, "Graz").Count, 1);
        }

        [Test]
        public void tourLogSearchTest()
        {
            TourLog l1 = new TourLog(0, 0, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l2 = new TourLog(0, 1, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l3 = new TourLog(0, 2, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Rain and Clouds", 10, "Bike", true, 2);
            List<TourLog> logs = new List<TourLog>();
            logs.Add(l1);
            logs.Add(l2);
            logs.Add(l3);
            Assert.AreEqual(TourSearch.searchTourLogs(logs, "Sunny").Count, 2);
        }

        //Export
        [Test]
        public void exportTourJSONTest()
        {
            Tour t = new Tour(0, "Tour 1", "Beschreibung", "img", "Wien", "Bregenz", 1.0f);
            Tour_Export te = new Tour_Export();

            try
            {
                te.JSON_Export(t, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "tourtest.json");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void exportTourCSVTest()
        {
            Tour t = new Tour(0, "Tour 1", "Beschreibung", "img", "Wien", "Bregenz", 1.0f);
            Tour_Export te = new Tour_Export();

            try
            {
                te.CSV_Export(t, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "test.csv");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void exportTourTXTTest()
        {
            Tour t = new Tour(0, "Tour 1", "Beschreibung", "img", "Wien", "Bregenz", 1.0f);
            Tour_Export te = new Tour_Export();

            try
            {
                te.TXT_Export(t, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "test.txt");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void exportTourLogsJSONTest()
        {
            TourLog l1 = new TourLog(0, 0, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l2 = new TourLog(0, 1, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l3 = new TourLog(0, 2, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Rain and Clouds", 10, "Bike", true, 2);
            List<TourLog> logs = new List<TourLog>();
            logs.Add(l1);
            logs.Add(l2);
            logs.Add(l3);

            TourLog_Export te = new TourLog_Export();

            try
            {
                te.JSON_Export(logs, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "tourlogstest.json");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void exportTourLogsCSVTest()
        {
            TourLog l1 = new TourLog(0, 0, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l2 = new TourLog(0, 1, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l3 = new TourLog(0, 2, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Rain and Clouds", 10, "Bike", true, 2);
            List<TourLog> logs = new List<TourLog>();
            logs.Add(l1);
            logs.Add(l2);
            logs.Add(l3);

            TourLog_Export te = new TourLog_Export();

            try
            {
                te.CSV_Export(logs, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "test.csv");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void exportTourLogsTXTTest()
        {
            TourLog l1 = new TourLog(0, 0, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l2 = new TourLog(0, 1, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l3 = new TourLog(0, 2, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Rain and Clouds", 10, "Bike", true, 2);
            List<TourLog> logs = new List<TourLog>();
            logs.Add(l1);
            logs.Add(l2);
            logs.Add(l3);

            TourLog_Export te = new TourLog_Export();

            try
            {
                te.TXT_Export(logs, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "test.txt");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        //Import Tests
        [Test]
        public void importTourJSONTest()
        {

            Tour_Import ti = new Tour_Import();

            try
            {
                Tour t = ti.JSON_Import(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "tourtest.json");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void importTourLogsJSONTest()
        {

            TourLog_Import tli = new TourLog_Import();

            try
            {
                List<TourLog> logs = tli.JSON_Import(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "tourlogstest.json");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        //PDF Export Tests

        [Test]
        public void exportTourReportTest()
        {
            PDF_Export px = new PDF_Export();
            Tour t = new Tour(0, "Tour 1", "Beschreibung", "test.jpg", "Wien", "Bregenz", 1.0f);

            TourLog l1 = new TourLog(0, 0, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l2 = new TourLog(0, 1, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l3 = new TourLog(0, 2, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Rain and Clouds", 10, "Bike", true, 2);
            List<TourLog> logs = new List<TourLog>();
            logs.Add(l1);
            logs.Add(l2);
            logs.Add(l3);

            try
            {
                px.exportTourReport(t, logs, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "test.pdf");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void exportSummarizeReportTest()
        {
            PDF_Export px = new PDF_Export();
            Tour t = new Tour(0, "Tour 1", "Beschreibung", "test.jpg", "Wien", "Bregenz", 1.0f);

            TourLog l1 = new TourLog(0, 0, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l2 = new TourLog(0, 1, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Sunny", 10, "Bike", true, 2);
            TourLog l3 = new TourLog(0, 2, "2020-01-02", "Fine", 23.02f, "1:34", 4, "Rain and Clouds", 10, "Bike", true, 2);
            List<TourLog> logs = new List<TourLog>();
            logs.Add(l1);
            logs.Add(l2);
            logs.Add(l3);

            try
            {
                px.exportTourSummarizeReport(t, logs, Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\" + "test.pdf");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        //Map Quest

        [Test]
        public void getRouteImageTest()
        {
            MapQuest mq = new MapQuest();

            try
            {
                mq.getRouteImage("Vienna", "Berlin");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void getTotalDistanceTest()
        {
            MapQuest mq = new MapQuest();

            try
            {
                mq.getTotalDistance("Vienna", "Berlin");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        //OpenWeather

        [Test]
        public void weatherTest()
        {
            OpenWeather ow = new OpenWeather();

            try
            {
                WeatherData wd = ow.getWeatherData("Berlin");
                return; // indicates success
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        //TourStatsTest

        [Test]
        public void getRecommendationRateTest()
        {
            TourStats ts = new TourStats(0, 1.2f, "2:24", 1.2f, 1.1f, "1:2", 1.1f, 1.1f, "1:1", 1.1f, 1, 2, 1.5f, 1,2, 1.5f, 5, 0);

            Assert.AreEqual(1f, ts.getRecommendationRate());
        }

        //Tour Tests
        [Test]
        public void getTourNameTest()
        {
            Tour t = new Tour(0, "Tour 1", "Beschreibung", "test.jpg", "Wien", "Bregenz", 1.0f);

            Assert.AreEqual("Tour 1", t.getTitle());
        }
        [Test]
        public void getTourFromTest()
        {
            Tour t = new Tour(0, "Tour 1", "Beschreibung", "test.jpg", "Wien", "Bregenz", 1.0f);

            Assert.AreEqual("Wien", t.getFrom());
        }
        [Test]
        public void getTourToTest()
        {
            Tour t = new Tour(0, "Tour 1", "Beschreibung", "test.jpg", "Wien", "Bregenz", 1.0f);

            Assert.AreEqual("Bregenz", t.getTo());
        }
        [Test]
        public void getTourTotalDist()
        {
            Tour t = new Tour(0, "Tour 1", "Beschreibung", "test.jpg", "Wien", "Bregenz", 1.0f);

            Assert.AreEqual(1.0f, t.getTotalDistance());
        }

    }
}
