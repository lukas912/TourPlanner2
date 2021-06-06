using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_DataAccess;
using TourPlanner_Model;

namespace TourPlanner_Business
{
    public class Data
    {
        DBConnection dbc = new DBConnection();
        MapQuest mq = new MapQuest();
        
        public List<Tour> getTours()
        {
            List<Tour> tours = dbc.getTours();

            foreach(Tour t in tours)
            {
                t.Image = mq.getRouteImage(t.From, t.To);
            }

            return tours;


        }
        public List<TourLog> getTourLogs()
        {
            return dbc.getTourLogs();
        }

        public bool addTour(Tour tour)
        {
            return dbc.addTour(new Tour(tour.ID, tour.Title, tour.Description, "img", tour.From, tour.To, mq.getTotalDistance(tour.From, tour.To)));
        }

        public bool addTourLog(TourLog tourlog)
        {
            return dbc.addTourLog(tourlog);
        }

        public bool deleteTour(int id)
        {
            return dbc.deleteTour(id);
        }

        public bool deleteTourLog(TourLog tourlog)
        {
            return dbc.deleteTourLog(tourlog);
        }

        public bool editTour(Tour tour)
        {
            return dbc.editTour(new Tour(tour.ID, tour.Title, tour.Description, "img", tour.From, tour.To, mq.getTotalDistance(tour.From, tour.To)));
        }

        public bool editTourLog(TourLog tourlog)
        {   
            if(validateTourLog(tourlog) == true)
            {
                return dbc.editTourLog(tourlog);
            }

            else
            {
                return false;
            }

        }

        private bool validateTourLog(TourLog tourlog)
        {

            if(tourlog.Difficulty <= 0 || tourlog.Difficulty > 10)
            {
                return false;
            }

            else if (tourlog.Distance <= 0)
            {
                return false;
            }

            else if(tourlog.Participants <= 0)
            {
                return false;
            }

            else if(tourlog.Rating < 1 || tourlog.Rating > 5)
            {
                return false;
            }

            else
            {
                return true;
            }

        }
    }
}
