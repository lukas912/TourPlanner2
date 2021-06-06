using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner_Model
{
    public class Tour
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public float TotalDistance { get; set; }

        public Tour(int id, string t, string d, string i, string from, string to, float td)
        {
            this.ID = id;
            this.Title = t;
            this.Description = d;
            this.Image = i;
            this.From = from;
            this.To = to;
            this.TotalDistance = td;
        }

        public string getTitle()
        {
            return this.Title;
        }

        public string getFrom()
        {
            return this.From;
        }

        public string getTo()
        {
            return this.To;
        }

        public float getTotalDistance()
        {
            return this.TotalDistance;
        }
    }
}
