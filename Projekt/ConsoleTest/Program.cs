using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;


namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var start = DateTime.Now;
            var teams = Dal.Instance.GetTeams();
            Console.WriteLine("GetTeams " + (DateTime.Now - start).TotalMilliseconds + "ms");

            foreach (var t in Dal.Instance.GetTeams())
            {
                Console.WriteLine(t);
            }
            Console.WriteLine();



            //start = DateTime.Now;
            //var team = Dal.GetTeamByFifaCode("CRO");
            //Console.WriteLine($"GetTeamByFifaCode {(DateTime.Now - start).TotalMilliseconds}ms");
            //Console.WriteLine(team);
            //Console.WriteLine();



            start = DateTime.Now;
            var allMatches = Dal.Instance.GetMatches();
            Console.WriteLine($"GetMatches {(DateTime.Now - start).TotalMilliseconds}ms");
            Console.WriteLine();


            //Dal.Instance.GetPlayersForTeam(Dal.Instance.GetTeamByFifaCode("CRO")).ToList().ForEach(Console.WriteLine);

            //Console.WriteLine();
            //start = DateTime.Now;
            //Dal.Instance
            //    .GetTeams()
            //    .ToList()
            //    .ForEach(x =>
            //    {
            //        Console.WriteLine();
            //        Console.WriteLine(x);
            //        Dal.Instance
            //        .GetPlayersForTeam(x)
            //        .ToList()
            //        .ForEach(Console.WriteLine);
            //    });
            //Console.WriteLine($"GetTeams().foreach(GetPlayersForTeam) {(DateTime.Now - start).TotalMilliseconds}ms");
            //Console.WriteLine();


            Dal.Instance.GetMatches().Select(x => x.Location).ToList().ForEach(Console.WriteLine);

        }
    }
}
