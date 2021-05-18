using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class Tour_Import
    {
        //Import Tour from JSON
        public Tour JSON_Import(string path)
        {
            // read file into a string and deserialize JSON to a type
            Tour tour = JsonConvert.DeserializeObject<Tour>(File.ReadAllText(path));

            return tour;
        }

        //Import Tour from CSV
        public Tour CSV_Import(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<Tour>();
                return records.ElementAtOrDefault(0);
            }
        }
    }
}
