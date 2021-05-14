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
    }
}
