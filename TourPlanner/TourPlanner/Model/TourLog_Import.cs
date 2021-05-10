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
    class TourLog_Import
    {
        //Import TourLog from JSON
        public List<TourLog> JSON_Import(string path)
        {
            string json = JsonConvert.DeserializeObject<string>(path);

            JArray a = JArray.Parse(json);

            IList<TourLog> tour_logs = a.ToObject<IList<TourLog>>();

            return tour_logs.ToList();
        }

        //Import TourLog from CSV
        public List<TourLog> CSV_Import(string path)
        {
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<TourLog>();
                return records.ToList();
            }

        }
    }
}
