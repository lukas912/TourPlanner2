using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    class Tour
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Tour(string t, string d)
        {
            this.Title = t;
            this.Description = d;
        }
    }
}
