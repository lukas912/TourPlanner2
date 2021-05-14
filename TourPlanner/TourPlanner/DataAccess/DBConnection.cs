using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;
using Npgsql;
using System.Configuration;

namespace TourPlanner.DataAccess
{
    class DBConnection
    {

        string CS;
        MapQuest mq = new MapQuest();

        private string getConnectionString()
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            return settings[0].ConnectionString;
            
        }

        //SELECTs
        public List<Tour> getTours()
        {
            CS = getConnectionString();

            // Connect to a PostgreSQL database
            List<Tour> output = new List<Tour>();
            using var con = new NpgsqlConnection(CS);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Tour\"", con))
            {

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        output.Add(new Tour(
                            reader.GetInt32(reader.GetOrdinal("tour_id")),
                            reader.GetString(reader.GetOrdinal("tour_name")),
                            reader.GetString(reader.GetOrdinal("tour_description")),
                            mq.getRouteImage(reader.GetString(reader.GetOrdinal("from")), reader.GetString(reader.GetOrdinal("to"))),
                            reader.GetString(reader.GetOrdinal("from")),
                            reader.GetString(reader.GetOrdinal("to"))

                            ));
                    }
                }


            }

            return output;
        }

        public List<TourLog> getTourLogs()
        {
            CS = getConnectionString();

            // Connect to a PostgreSQL database
            List<TourLog> output = new List<TourLog>();
            using var con = new NpgsqlConnection(CS);
            con.Open();

            using (var cmd = new NpgsqlCommand("SELECT * FROM \"Tour_Log\"", con))
            {

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        output.Add(new TourLog(
                            reader.GetInt32(reader.GetOrdinal("tour_id")),
                            reader.GetInt32(reader.GetOrdinal("tour_log_id")),
                            reader.GetString(reader.GetOrdinal("timestamp")),
                            reader.GetString(reader.GetOrdinal("report")),
                            reader.GetFloat(reader.GetOrdinal("distance")),
                            reader.GetString(reader.GetOrdinal("total_time")),
                            reader.GetInt32(reader.GetOrdinal("rating"))
                            ));
                    }
                }

            } 

            return output;
        }

        //INSERTs

        public bool addTour(Tour tour)
        {
            throw new NotImplementedException();
        }

        public bool addTourLog(TourLog tourlog)
        {
            throw new NotImplementedException();
        }

        //UPDATEs

        public bool editTour(Tour tour)
        {
            throw new NotImplementedException();
        }

        public bool editTourLog(TourLog tourlog)
        {
            throw new NotImplementedException();
        }

        //DELETEs

        public bool deleteTour(Tour tour)
        {
            throw new NotImplementedException();
        }

        public bool deleteTourLog(TourLog tourlog)
        {
            throw new NotImplementedException();
        }


    }
}
