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
using TourPlanner_Model;

namespace TourPlanner_Business
{
    public class TourLog_Import
    {
        //Import TourLog from JSON
        public List<TourLog> JSON_Import(string path, int id)
        {
            string json = System.IO.File.ReadAllText(path);
            var tour_logs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TourLog>>(json);

            foreach(TourLog item in tour_logs)
            {
                item.TourID = id;
            }

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
