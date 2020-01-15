using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class WebJsonRepo : IJsonRepo
    {
        private readonly string URL_TEAMS = "http://world-cup-json-2018.herokuapp.com/teams";
        private readonly string URL_RESULTS = "http://world-cup-json-2018.herokuapp.com/teams/results";
        private readonly string URL_MATCHES = "http://world-cup-json-2018.herokuapp.com/matches";//country?fifa_code=";

        public string GetTeamsJson()
        {
            return GetJsonFromUrl(URL_TEAMS);
        }

        public string GetMatchesJson()
        {
            return GetJsonFromUrl(URL_MATCHES);
        }

        public string GetResultsJson()
        {
            return GetJsonFromUrl(URL_RESULTS);
        }

        private string GetJsonFromUrl(string url)
        {
            try
            {
                return new RestClient(url).Execute(new RestRequest()).Content;
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
            return string.Empty;
        }
    }
}
