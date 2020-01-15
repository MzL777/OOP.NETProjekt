using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    public interface IJsonRepo
    {
        string GetTeamsJson();
        string GetMatchesJson();
        string GetResultsJson();
    }
}
