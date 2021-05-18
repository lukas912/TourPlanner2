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

        public float MIN_Distance { get; set; }
        public string MIN_Time { get; set; }
        public float MIN_Rating { get; set; }

        public float MAX_Distance { get; set; }
        public string MAX_Time { get; set; }
        public float MAX_Rating { get; set; }



        public TourStats(int id, float dist, string time, float rating, float md, string mt, float mr, float md2, string mt2, float mr2)
        {
            this.ID = id;

            this.AVG_Distance = dist;
            this.AVG_Time = time;
            this.AVG_Rating = rating;

            this.MIN_Distance = md;
            this.MIN_Time = mt;
            this.MIN_Rating = mr;

            this.MAX_Distance = md2;
            this.MAX_Time = mt2;
            this.MAX_Rating = mr2;
        }
    }
}
