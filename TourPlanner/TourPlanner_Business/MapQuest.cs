using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;
using System.Net;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TourPlanner_Business
{
    public class MapQuest
    {
        public string KEY = "Iyuj6MgMp3OZEU59aDLg5Si0oyXgC2l0";
        public string BASE_URL = "https://www.mapquestapi.com/staticmap/v5/map?start=";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public string getRouteImage(string from, string to)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/" + from + "_" + to + ".jpg";

                if (!File.Exists(path))
                {
                    string url = BASE_URL + from + "&end=" + to + "&size=600,400@2x&key=" + KEY;
                    var client = new RestClient(url);

                    var request = new RestRequest();

                    var response = client.Get(request);

                    var fileBytes = client.DownloadData(request);

                    File.WriteAllBytes(path, fileBytes);

                }

                return path;
            }

            catch(Exception ex)
            {
                log.Error(ex);
                return "";
            }

        }

        public float getTotalDistance(string from, string to)
        {
            try
            {
                string url = "http://www.mapquestapi.com/directions/v2/route?key=" + KEY + "&from=" + from + "&to=" + to;
                var client = new RestClient(url);

                var request = new RestRequest();

                var response = client.Get(request);

                JObject obj = JObject.Parse(response.Content);

                return (float)obj["route"]["distance"];
            }

            catch(Exception ex)
            {
                log.Error(ex);
                return 0;
            }



        }


    }
}
