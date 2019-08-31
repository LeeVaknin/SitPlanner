using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Controllers
{
    public static class MyGlobals
    {
        public static int GlobalEventID;

        static MyGlobals() { GlobalEventID = 1234; }

        public static int MyProperty { get; set; }

        

        public static void SetEventID(int id)
        {
            GlobalEventID = id;
        }
    }
}
