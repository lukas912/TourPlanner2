using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CsvHelper;
using System.Globalization;

namespace TourPlanner.Model
{
    class TourLog_Export
    {
        //Export Tour Log to JSON
        public void JSON_Export(List<TourLog> tour_logs, string path)
        {
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(path, JsonConvert.SerializeObject(tour_logs));
        }

        //Export Tour Log to CSV
        public void CSV_Export(List<TourLog> tour_logs, string path)
        {
            var records = tour_logs;

            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }

        //Export to txt file
        public void TXT_Export(List<TourLog> tour_logs, string path)
        {
            string text = "";
            foreach(TourLog tl in tour_logs)
                text += "ID: " + tl.TourID + "\n" +
                        "Log ID: " + tl.TourLogID + "\n" +
                        "Timestamp: " + tl.Timestamp + "\n" +
                        "Report: " + tl.Report + "\n" +
                        "Distance: " + tl.Distance + "\n" +
                        "Total Time: " + tl.TotalTime + "\n" +
                        "Rating: " + tl.Rating + "\n\n";

            File.WriteAllTextAsync(path, text);
        }
    }
}
