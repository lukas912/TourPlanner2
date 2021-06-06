using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner_Model;

namespace TourPlanner_Business
{
    public class MyTourSearch
    {
        private static MyTourSearch instance = new MyTourSearch();
        private MyTourSearch() { }

        static Data data = new Data();

        public static MyTourSearch Instance
        {
            get {return instance;}
        }

        public void DoSomething()
        {
            Debug.Print("Singleton class code has been called");
        }

        public List<Tour> searchTours(List<Tour> tours, string input)
        {
            List<Tour> output = new List<Tour>();

            if (input == "")
            {
                return data.getTours();
            }

            else
            {
                foreach (Tour tour in tours)
                {
                    if (tour.Title.Contains(input))
                    {
                        output.Add(tour);
                    }

                    else if (tour.Description.Contains(input))
                    {
                        output.Add(tour);
                    }

                    else if (tour.From.Contains(input))
                    {
                        output.Add(tour);
                    }

                    else if (tour.To.Contains(input))
                    {
                        output.Add(tour);
                    }
                }
            }
            Debug.Print("Singleton class code has been called");
            return output;
        }

        public List<TourLog> searchTourLogs(List<TourLog> tour_logs, string input)
        {
            List<TourLog> output = new List<TourLog>();

            if (input == "")
            {
                return data.getTourLogs();
            }

            else
            {
                foreach (TourLog tourlog in tour_logs)
                {
                    if (tourlog.Report.Contains(input))
                    {
                        output.Add(tourlog);
                    }

                    else if (tourlog.Vehicle.Contains(input))
                    {
                        output.Add(tourlog);
                    }

                    else if (tourlog.Weather.Contains(input))
                    {
                        output.Add(tourlog);
                    }

                }
            }
            Debug.Print("Singleton class code has been called");
            return output;
        }
    }
}
