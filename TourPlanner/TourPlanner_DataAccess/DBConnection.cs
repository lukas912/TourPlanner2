using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;
using TourPlanner_Model;

namespace TourPlanner_DataAccess
{
    public class DBConnection
    {

        string CS;

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
                            "image",
                            reader.GetString(reader.GetOrdinal("from")),
                            reader.GetString(reader.GetOrdinal("to")),
                            reader.GetFloat(reader.GetOrdinal("total_distance"))
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
                            reader.GetInt32(reader.GetOrdinal("rating")),
                            reader.GetString(reader.GetOrdinal("weather")),
                            reader.GetInt32(reader.GetOrdinal("difficulty")),
                            reader.GetString(reader.GetOrdinal("vehicle")),
                            reader.GetBoolean(reader.GetOrdinal("recommendation")),
                            reader.GetInt32(reader.GetOrdinal("participants"))
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
                using (var cmd = new NpgsqlCommand("INSERT INTO \"Tour\" (tour_id, tour_name, tour_description, \"from\", \"to\", total_distance) VALUES (@id, @name, @desc, @from, @to, @td)", con))
                {
                    cmd.Parameters.AddWithValue("id", tour.ID);
                    cmd.Parameters.AddWithValue("name", tour.Title);
                    cmd.Parameters.AddWithValue("desc", tour.Description);
                    cmd.Parameters.AddWithValue("from", tour.From);
                    cmd.Parameters.AddWithValue("to", tour.To);
                    cmd.Parameters.AddWithValue("td", tour.TotalDistance);
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
                using (var cmd = new NpgsqlCommand("UPDATE \"Tour\" SET tour_name = @title, tour_description = @desc, \"from\" = @from, \"to\" = @to, total_distance = @td WHERE tour_id = @id;", con))
                {
                    cmd.Parameters.AddWithValue("id", tour.ID);
                    cmd.Parameters.AddWithValue("title", tour.Title);
                    cmd.Parameters.AddWithValue("desc", tour.Description);
                    cmd.Parameters.AddWithValue("from", tour.From);
                    cmd.Parameters.AddWithValue("to", tour.To);
                    cmd.Parameters.AddWithValue("td", tour.TotalDistance);

                    cmd.ExecuteNonQuery();
                }

                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool editTourLog(TourLog tourlog)
        {
            CS = getConnectionString();

            // Connect to a PostgreSQL database
            using var con = new NpgsqlConnection(CS);
            con.Open();

            try
            {
                using (var cmd = new NpgsqlCommand("UPDATE \"Tour_Log\" SET timestamp = @ts, report = @rp, distance = @ds, total_time = @tt, rating = @rt WHERE tour_id = @tid AND tour_log_id = @tlid;", con))
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
