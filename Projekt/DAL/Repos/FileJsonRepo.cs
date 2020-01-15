using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public class FileJsonRepo : IJsonRepo
    {
        private readonly string PATH_TEAMS = "./Json/teams.json";
        private readonly string PATH_RESULTS = "./Json/results.json";
        private readonly string PATH_MATCHES = "./Json/matches.json";

        public string GetMatchesJson()
        {
            return GetJsonFromFile(PATH_MATCHES);
        }

        public string GetResultsJson()
        {
            return GetJsonFromFile(PATH_RESULTS);
        }

        public string GetTeamsJson()
        {
            return GetJsonFromFile(PATH_TEAMS);
        }

        private string GetJsonFromFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
            }
            return string.Empty;
        }
    }
}
