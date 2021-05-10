using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    class Tour
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public Tour(int id, string t, string d, string i)
        {
            this.ID = id;
            this.Title = t;
            this.Description = d;
            this.Image = i;
        }
    }
}
