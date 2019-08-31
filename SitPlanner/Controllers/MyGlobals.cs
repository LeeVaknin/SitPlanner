using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Controllers
{
    public static class MyGlobals
    {
        public static int GlobalEventID;
        public static string GlobalEventName;

        static MyGlobals()
        {
            GlobalEventID = 0;
            GlobalEventName = "Choose Event";
        }

        public static void SetEventID(int id)
        {
            GlobalEventID = id;
        }

        public static void SetEventName(string eventName)
        {
            GlobalEventName = eventName;
        }
    }
}
