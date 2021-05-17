using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccess;

namespace TourPlanner.Model
{
    class Data
    {
        DBConnection dbc = new DBConnection();
        
        public List<Tour> getTours()
        {
            return dbc.getTours();
        }
        public List<TourLog> getTourLogs()
        {
            return dbc.getTourLogs();
        }

        public bool addTour(Tour tour)
        {
            return dbc.addTour(tour);
        }

        public bool addTourLog(TourLog tourlog)
        {
            return dbc.addTourLog(tourlog);
        }
    }
}
