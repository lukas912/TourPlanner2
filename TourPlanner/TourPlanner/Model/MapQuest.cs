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

namespace TourPlanner.Model
{
    class MapQuest
    {
        public string KEY = "Iyuj6MgMp3OZEU59aDLg5Si0oyXgC2l0";
        public string BASE_URL = "https://www.mapquestapi.com/staticmap/v5/map?start=";


        public string getRouteImage(string from, string to)
        {
            string url = BASE_URL + from + "&end=" + to + "&size=600,400@2x&key=" + KEY;
            var client = new RestClient(url);

            var request = new RestRequest();

            var response = client.Get(request);

            Debug.Print(response.Content);

            var fileBytes = client.DownloadData(request);
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "/" + from + "_" + to + ".jpg";
            File.WriteAllBytes(path, fileBytes);


            return path;
        }


    }
}
