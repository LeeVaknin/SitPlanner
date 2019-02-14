using SitPlanner.Models.Enums;
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
    

        public List<Restriction> Restrictions { get; set; }

        public List<Invitee> Invitees { get; set; }
    }
}
