using DAL.Model;
using DAL.Model.Matches;
using DAL.Model.Results;
using DAL.Model.Teams;
using DAL.Repos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Dal
    {
        private string[] fileData = new string[2] { "hr", "false" };

        private IList<Team> Teams;
        private IList<Match> Matches;
        private IList<Result> Results;

        public Exception GetLastExceptionInfo { get; set; }

        private IJsonRepo Repository = JsonRepoFactory.GetRepo();

        private static readonly Dal instance = new Dal();
        private readonly string SETTINGS_FILE_PATH = "settings.txt";
        private readonly string FAVOURITES_FILE_PATH = "favourites.txt";
        private readonly string IMAGES_FILE_PATH = "imagepaths.txt";

        public static Dal Instance
        {
            get => instance;
        }

        static Dal() { }
        private Dal() { }


        public Team GetTeamByFifaCode(string fifaCode)
        {
            return GetTeams()?.FirstOrDefault(x => x.FifaCode.Equals(fifaCode));
        }
        public IEnumerable<Team> GetTeams()
        {
            if (Teams == null)
            {
                Teams = Team.FromJson(Repository.GetTeamsJson());
            }
            return Teams;
        }


        public IEnumerable<Match> GetMatches()
        {
            if (Matches == null)
            {
                Matches = Match.FromJson(Repository.GetMatchesJson());
            }
            return Matches;
        }
        public IEnumerable<Match> GetMatchesForTeam(string fifaCode)
        {
            return GetMatchesForTeam(GetTeamByFifaCode(fifaCode));
        }
        public IEnumerable<Match> GetMatchesForTeam(Team team)
        {
            return GetMatches()?.Where(x => x.HomeTeam.Equals(team) || x.AwayTeam.Equals(team));
        }
        public Match GetMatchForTeams(Team team1, Team team2)
        {
            return GetMatches()?.Where(x => (x.HomeTeam.Equals(team1) && x.AwayTeam.Equals(team2)) || (x.AwayTeam.Equals(team1) && x.HomeTeam.Equals(team2)))?.First();
        }


        public IEnumerable<Team> GetOpponentsForTeam(string fifaCode)
        {
            return GetOpponentsForTeam(GetTeamByFifaCode(fifaCode));
        }
        public IEnumerable<Team> GetOpponentsForTeam(Team team)
        {
            foreach (var match in GetMatchesForTeam(team))
            {
                if (match.HomeTeam.Equals(team))
                {
                    yield return GetTeamByFifaCode(match.AwayTeam.FifaCode);
                }
                else
                {
                    yield return GetTeamByFifaCode(match.HomeTeam.FifaCode);
                }
            }

        }


        public IEnumerable<Player> GetPlayersForTeam(string fifaCode)
        {
            return GetPlayersForTeam(GetTeamByFifaCode(fifaCode));
        }
        public IEnumerable<Player> GetPlayersForTeam(Team team)
        {
            if (team == null)
            {
                return null;
            }

            if (team.Players != null)
            {
                return team.Players;
            }
            Match firstMatch = GetMatchesForTeam(team)?.First();

            Teams.Where(x => x.Equals(team)).FirstOrDefault().Players = firstMatch.HomeTeam.Equals(team) ?
                firstMatch.HomeTeamStatistics.StartingEleven.Union(firstMatch.HomeTeamStatistics.Substitutes) :
                firstMatch.AwayTeamStatistics.StartingEleven.Union(firstMatch.AwayTeamStatistics.Substitutes);

            LoadPlayerStatisticsForTeam(team);

            return Teams.Where(x => x.Equals(team)).FirstOrDefault().Players;
        }


        public IEnumerable<Result> GetResults()
        {
            if (Results == null)
            {
                Results = Result.FromJson(Repository.GetResultsJson());
            }
            return Results;
        }
        public Result GetResultsForTeam(string fifaCode)
        {
            return GetResultsForTeam(GetTeamByFifaCode(fifaCode));
        }
        public Result GetResultsForTeam(Team team)
        {
            return GetResults()?.First(x => x.Country.Equals(team.Country));
        }


        public int GetPlayerYellowCardsForMatch(Player player, Match match)
        {
            int count = 0;
            foreach (var evnt in match.AwayTeamEvents.Union(match.HomeTeamEvents))
            {
                if (evnt.Player.Equals(player.Name) && evnt.TypeOfEvent == TypeOfEvent.YellowCard || evnt.TypeOfEvent == TypeOfEvent.YellowCardSecond)
                {
                    count++;
                }
            }
            return count;
        }
        public int GetPlayerGoalsForMatch(Player player, Match match)
        {
            int count = 0;
            foreach (var evnt in match.AwayTeamEvents.Union(match.HomeTeamEvents))
            {
                if (evnt.Player.Equals(player.Name) && evnt.TypeOfEvent == TypeOfEvent.Goal || evnt.TypeOfEvent == TypeOfEvent.GoalOwn || evnt.TypeOfEvent == TypeOfEvent.GoalPenalty)
                {
                    count++;
                }
            }
            return count;
        }
        private bool LoadPlayerStatisticsForTeam(Team team)
        {
            try
            {
                var matches = GetMatchesForTeam(team);
                foreach (var match in matches)
                {
                    var isTeamHomeTeam = match.HomeTeam.Equals(team);
                    if (isTeamHomeTeam)
                    {
                        foreach (var teamEvent in match.HomeTeamEvents)
                        {
                            switch (teamEvent.TypeOfEvent)
                            {
                                case TypeOfEvent.Goal:
                                case TypeOfEvent.GoalOwn:
                                case TypeOfEvent.GoalPenalty:
                                    Teams.Where(x => x.Equals(team)).FirstOrDefault().Players.First(x => x.Name.Equals(teamEvent.Player)).Goals++;
                                    break;

                                case TypeOfEvent.YellowCard:
                                case TypeOfEvent.YellowCardSecond:
                                    Teams.Where(x => x.Equals(team)).FirstOrDefault().Players.First(x => x.Name.Equals(teamEvent.Player)).YellowCards++;
                                    break;

                                //case TypeOfEvent.RedCard:
                                //case TypeOfEvent.SubstitutionIn:
                                //case TypeOfEvent.SubstitutionOut:
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var teamEvent in match.AwayTeamEvents)
                        {
                            switch (teamEvent.TypeOfEvent)
                            {
                                case TypeOfEvent.Goal:
                                case TypeOfEvent.GoalOwn:
                                case TypeOfEvent.GoalPenalty:
                                    Teams.Where(x => x.Equals(team)).FirstOrDefault().Players.First(x => x.Name.Equals(teamEvent.Player)).Goals++;
                                    break;

                                case TypeOfEvent.YellowCard:
                                case TypeOfEvent.YellowCardSecond:
                                    Teams.Where(x => x.Equals(team)).FirstOrDefault().Players.First(x => x.Name.Equals(teamEvent.Player)).YellowCards++;
                                    break;

                                //case TypeOfEvent.RedCard:
                                //case TypeOfEvent.SubstitutionIn:
                                //case TypeOfEvent.SubstitutionOut:
                                default:
                                    break;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                GetLastExceptionInfo = e; // TODO
                return false;
            }
        }


        public IDictionary<string, string> LoadImagesFromFile()
        {
            var data = new Dictionary<string, string>();

            try
            {
                var lines = File.ReadAllLines(IMAGES_FILE_PATH);
                foreach (var line in lines)
                {
                    var split = line.Split('=');
                    if (split.Length == 2)
                    {
                        data.Add(split[0], split[1]);
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                GetLastExceptionInfo = e;
                return new Dictionary<string, string>();
            }

        }
        public IDictionary<string, string> ReadFavouriteTeamAndPlayersFromFile()
        {
            string team = string.Empty;
            string p1 = string.Empty, p2 = string.Empty, p3 = string.Empty;
            try
            {
                var lines = File.ReadAllLines(FAVOURITES_FILE_PATH);
                foreach (var line in lines)
                {
                    var split = line.Split('=');

                    switch (split[0])
                    {
                        case "team":
                            team = split[1];
                            break;
                        case "p1":
                            p1 = split[1];
                            break;
                        case "p2":
                            p2 = split[1];
                            break;
                        case "p3":
                            p3 = split[1];
                            break;
                        default:
                            break;
                    }
                }
                var data = new Dictionary<string, string>();
                if (team != string.Empty) data["team"] = team;
                if (p1 != string.Empty) data["p1"] = p1;
                if (p2 != string.Empty) data["p2"] = p2;
                if (p3 != string.Empty) data["p3"] = p3;
                return data;
            }
            catch (Exception e)
            {
                GetLastExceptionInfo = e;
                var x = new Dictionary<string, string>();
                x["team"] = GetTeams().First().ToString();
                return x;
            }
        }


        public IEnumerable<Language> GetLanguages()
        {
            return Enum.GetValues(typeof(Language)).Cast<Language>();
        }

        public string ReadLanguageFromFile()
        {
            ReadSettingsFromFile();
            return fileData[0];
        }
        private void ReadSettingsFromFile()
        {
            try
            {
                var lines = File.ReadAllLines(SETTINGS_FILE_PATH);
                foreach (var line in lines)
                {
                    var split = line.Split('=');
                    if(split.Count() < 2)
                    {
                        continue;
                    }
                    if (split[0].Equals("lang"))
                    {
                        if (split[1].Trim() == string.Empty)
                        {
                            fileData[0] = "false";
                        }
                        else
                        {
                            fileData[0] = split[1];
                        }
                    }
                    if (split[0].Equals("fullscreen"))
                    {
                        if (split[1].Trim() == string.Empty)
                        {
                            fileData[1] = "hr";
                        }
                        else
                        {
                            fileData[1] = split[1];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                GetLastExceptionInfo = e;
            }
        }
        public string ReadFullscreenFromFile()
        {
            ReadSettingsFromFile();
            return fileData[1];
        }

        public bool SaveLanguageToFile(string language)
        {
            try
            {
                string outS = $"lang={language}{Environment.NewLine}fullscreen={fileData[1]}";
                File.WriteAllText(SETTINGS_FILE_PATH, outS);
                return true;
            }
            catch (Exception e)
            {
                GetLastExceptionInfo = e;
                return false;
            }
        }
        public bool SaveSettingsToFile(string language, string fullscreen)
        {
            try
            {
                string outS = $"lang={language}{Environment.NewLine}fullscreen={fullscreen}";
                File.WriteAllText(SETTINGS_FILE_PATH, outS);
                return true;
            }
            catch (Exception e)
            {
                GetLastExceptionInfo = e;
                return false;
            }
        }
        public bool SaveFavouritesToFile(string[] favourites)
        {
            try
            {
                File.WriteAllLines(FAVOURITES_FILE_PATH, favourites);
                return true;
            }
            catch (Exception e)
            {
                GetLastExceptionInfo = e;   //TODO
                return false;
            }
        }
        public bool SaveImagesToFile(IDictionary<string, string> images)
        {
            var strings = new List<string>();

            images.Keys.ToList().ForEach(x => strings.Add(x + "=" + images[x]));

            try
            {
                File.WriteAllLines(IMAGES_FILE_PATH, strings.ToArray());
                return true;
            }
            catch (Exception e)
            {
                GetLastExceptionInfo = e;   //TODO
                return false;
            }
        }

    }
}
