using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitPlanner.Models;

namespace SitPlanner.Algo
{
    public class AlgoDb
    {
        public List<Invitee> invitees;
        public List<Table> tables;
        public List<PersonalRestriction> personalRestrictions;
        public List<AccessibilityRestriction> accessibilityRestrictions;

        public AlgoDb(List<Invitee> invitees, List<Table> tables, List<PersonalRestriction> personalRestrictions, 
            List<AccessibilityRestriction> accessibilityRestrictions, List<Category> categories)
        {
            this.invitees = new List<Invitee>(invitees);
            this.tables = new List<Table>(tables);
            this.personalRestrictions = new List<PersonalRestriction>(personalRestrictions);
            this.accessibilityRestrictions = new List<AccessibilityRestriction>(accessibilityRestrictions);
        }
    }
}
