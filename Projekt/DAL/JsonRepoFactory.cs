using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class JsonRepoFactory
    {
        private static IJsonRepo instance;
        private static bool getFromWeb = true;  //TODO REPLACE

        public static IJsonRepo GetRepo()
        {
            if (instance == null)
            {
                if (getFromWeb)
                {
                    instance = new WebJsonRepo();
                }
                else
                {
                    instance = new FileJsonRepo();
                }
            }
            return instance;
        }
    }
}
