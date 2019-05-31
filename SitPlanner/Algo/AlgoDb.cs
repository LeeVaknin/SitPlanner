using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitPlanner.Models;

namespace SitPlanner.Algo
{
    public class AlgoDb
    {
        public List<Invitee> invitees = new List<Invitee>();
        public List<Table> tables = new List<Table>();
        public List<PersonalRestriction> personalRestrictions = new List<PersonalRestriction>();
        public List<AccessibilityRestriction> accessibilityRestrictions = new List<AccessibilityRestriction>();
        public List<Category> categories = new List<Category>();

        public AlgoDb(List<Invitee> invitees, List<Table> tables, List<PersonalRestriction> personalRestrictions, 
            List<AccessibilityRestriction> accessibilityRestrictions, List<Category> categories)
        {
            this.invitees = invitees;
            this.tables = tables;
            this.personalRestrictions = personalRestrictions;
            this.accessibilityRestrictions = accessibilityRestrictions;
            this.categories = categories;
        }
    }
}
