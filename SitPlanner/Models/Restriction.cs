﻿using SitPlanner.Models.Enums;
using SitPlanner.Models.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public abstract class Restriction
    {
        public IList<InviteeRestriction> InviteeRestrictions { get; set; }

        public IList<TableRestriction> TableRestrictions { get; set; }

    }
}
