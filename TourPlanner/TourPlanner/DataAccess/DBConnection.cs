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
            CS = getConnectionString();

            // Connect to a PostgreSQL database
            using var con = new NpgsqlConnection(CS);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO \"Tour\" (tour_id, tour_name, tour_description, \"from\", \"to\") VALUES (@id, @name, @desc, @from, @to)", con))
                {
                    cmd.Parameters.AddWithValue("id", tour.ID);
                    cmd.Parameters.AddWithValue("name", tour.Title);
                    cmd.Parameters.AddWithValue("desc", tour.Description);
                    cmd.Parameters.AddWithValue("from", tour.From);
                    cmd.Parameters.AddWithValue("to", tour.To);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch
            {
                return false;
            }


        }

        public bool addTourLog(TourLog tourlog)
        {
            CS = getConnectionString();

            // Connect to a PostgreSQL database
            using var con = new NpgsqlConnection(CS);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("INSERT INTO \"Tour_Log\" (tour_id, tour_log_id, timestamp, report, distance, total_time, rating) VALUES (@tid, @tlid, @ts, @rp, @ds, @tt, @rt)", con))
                {
                    cmd.Parameters.AddWithValue("tid", tourlog.TourID);
                    cmd.Parameters.AddWithValue("tlid", tourlog.TourLogID);
                    cmd.Parameters.AddWithValue("ts", tourlog.Timestamp);
                    cmd.Parameters.AddWithValue("rp", tourlog.Report);
                    cmd.Parameters.AddWithValue("ds", tourlog.Distance);
                    cmd.Parameters.AddWithValue("tt", tourlog.TotalTime);
                    cmd.Parameters.AddWithValue("rt", tourlog.Rating);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch
            {
                return false;
            }
        }

        //UPDATEs

        public bool editTour(Tour tour)
        {
            CS = getConnectionString();

            // Connect to a PostgreSQL database
            using var con = new NpgsqlConnection(CS);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("UPDATE \"Tour\" SET tour_name = @title, tour_description = @desc, \"from\" = @from, \"to\" = @to WHERE tour_id = @id;", con))
                {
                    cmd.Parameters.AddWithValue("id", tour.ID);
                    cmd.Parameters.AddWithValue("title", tour.Title);
                    cmd.Parameters.AddWithValue("desc", tour.Description);
                    cmd.Parameters.AddWithValue("from", tour.From);
                    cmd.Parameters.AddWithValue("to", tour.To);

                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool editTourLogs(List<TourLog> tourlogs)
        {
            throw new NotImplementedException();
        }

        //DELETEs

        public bool deleteTour(int id)
        {
            CS = getConnectionString();

            // Connect to a PostgreSQL database
            using var con = new NpgsqlConnection(CS);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("DELETE FROM \"Tour\" WHERE tour_id = @id", con))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool deleteTourLog(TourLog tourlog)
        {
            CS = getConnectionString();

            // Connect to a PostgreSQL database
            using var con = new NpgsqlConnection(CS);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("DELETE FROM \"Tour_Log\" WHERE tour_id = @id AND tour_log_id = @logid", con))
                {
                    cmd.Parameters.AddWithValue("id", tourlog.TourID);
                    cmd.Parameters.AddWithValue("logid", tourlog.TourLogID);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch
            {
                return false;
            }
        }


    }
}
