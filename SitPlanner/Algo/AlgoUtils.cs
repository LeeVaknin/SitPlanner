using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Algo
{
    public class AlgoUtils
    {
        public int AlgoRandom(int limit)
        {
            Random rn = new Random();
            int ran = rn.Next(0, limit);

            return ran;
        }
    }
}
