using SitPlanner.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class LocationRestriction: Restriction
    {
        public int Id { get; set; }

        public LocationType LocationType { get; set; }

        //not sure if needed
        //public List<Table> Tables { get; set; }

    }
}
