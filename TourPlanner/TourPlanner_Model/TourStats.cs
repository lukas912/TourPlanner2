using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Model
{
    public class TourStats
    {
        public int ID { get; set; }
        public float AVG_Distance { get; set; }
        public string AVG_Time { get; set; }
        public float AVG_Rating { get; set; }
        public float AVG_Difficulty { get; set; }
        public float AVG_Participants { get; set; }

        public float MIN_Distance { get; set; }
        public string MIN_Time { get; set; }
        public float MIN_Rating { get; set; }
        public int MIN_Difficulty { get; set; }
        public int MIN_Participants { get; set; }

        public float MAX_Distance { get; set; }
        public string MAX_Time { get; set; }
        public float MAX_Rating { get; set; }
        public int MAX_Difficulty { get; set; }
        public int MAX_Participants { get; set; }

        public float RecommendationRate { get; set; }



        public TourStats(int id, float dist, string time, float rating, float md, string mt, float mr, float md2, string mt2, float mr2, int df_min, int df_max, float df_avg, int pa_min, int pa_max, float pa_avg, int rc_true, int rc_false)
        {
            this.ID = id;

            this.AVG_Distance = dist;
            this.AVG_Time = time;
            this.AVG_Rating = rating;
            this.AVG_Difficulty = df_avg;
            this.AVG_Participants = pa_avg;

            this.MIN_Distance = md;
            this.MIN_Time = mt;
            this.MIN_Rating = mr;
            this.MIN_Difficulty = df_min;
            this.MIN_Participants = pa_min;

            this.MAX_Distance = md2;
            this.MAX_Time = mt2;
            this.MAX_Rating = mr2;
            this.MAX_Difficulty = df_max;
            this.MAX_Participants = pa_max;

            this.RecommendationRate = rc_true / (rc_true + rc_false);
        }

        public float getRecommendationRate()
        {
            return this.RecommendationRate;
        }
    }
}
