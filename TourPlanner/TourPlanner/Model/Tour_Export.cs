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
    class Tour_Export
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
    }
}
