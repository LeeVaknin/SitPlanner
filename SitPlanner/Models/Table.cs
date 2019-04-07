using SitPlanner.Models.Enums;
using SitPlanner.Models.ManyToMany;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class Table
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide table capacity")]
        [Display(Name = "Tabale capacity")]
        public int CapacityOfPeople { get; set; }

        
        [Display(Name = "Tabale type")]
        public TableType TableType { get; set; }

        //public int EventId { get; set; }
        public virtual Event Event { get; set; }

        //public IList<Restriction> Restrictions { get; set; }
        //public IList<TableRestriction> TableRestrictions { get; set; }

        //public List<Invitee> Invitees { get; set; }
        //public IList<InviteeTable> InviteeTables { get; set; }
    }
}
