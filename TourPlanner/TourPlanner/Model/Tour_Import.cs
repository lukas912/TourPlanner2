using CsvHelper;
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
    class Tour_Import
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
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Tour>();
                return records.ElementAt(0);
            }

        }
    }
}
