using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Model
{
    public class TourLog
    {
        public int TourID { get; set; }
        public int TourLogID { get; set; }
        public string Timestamp { get; set; }
        public string Report { get; set; }
        public float Distance { get; set; }
        public string TotalTime { get; set; }
        public int Rating { get; set; }
        public string Weather { get; set; }
        public int Difficulty { get; set; }
        public string Vehicle { get; set; }
        public bool Recommendation { get; set; }
        public int Participants { get; set; }

        public TourLog(int tid, int logid, string timestamp, string report, float distance, string total_time, int rating, string weather, int difficulty, string vehicle, bool recommendation, int participants)
        {
            this.TourID = tid;
            this.TourLogID = logid;
            this.Timestamp = timestamp;
            this.Report = report;
            this.Distance = distance;
            this.TotalTime = total_time;
            this.Rating = rating;
            this.Weather = weather;
            this.Difficulty = difficulty;
            this.Vehicle = vehicle;
            this.Recommendation = recommendation;
            this.Participants = participants;
        }
    }
}
