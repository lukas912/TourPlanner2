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
    public class Tour_Export
    {
        //Export Tour to JSON
        public void JSON_Export(Tour tour, string path)
        {
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(path, JsonConvert.SerializeObject(tour));
        }

        //Export Tour to CSV
        public void CSV_Export(Tour tour, string path)
        {
            var records = new List<Tour>();
            records.Add(tour);

            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }

        //Export to txt file
        public void TXT_Export(Tour tour, string path)
        {
            string text = "ID: " + tour.ID + "\n" +
                "Title: " + tour.Title + "\n" +
                "Description: " + tour.Description + "\n" +
                "Image: " + tour.Image;

            File.WriteAllTextAsync(path, text);
        }
    }
}
