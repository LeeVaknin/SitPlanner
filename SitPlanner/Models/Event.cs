using SitPlanner.Models.ManyToMany;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide event name")]
        [Display(Name = "Event name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        // --- Validate we need it ---------------
        public int NumOfTables { get; set; }


        //----------------------------------------------

        public IList<Arrangement> Arrangements { get; set; }

        public IList<UserEvent> UserEvents { get; set; }


        // TODO: Configure one-to-many with invitee
        public IList<Invitee> Invitees { get; set; }

        [NotMapped]
        public int NumOfInvitees => Invitees.Count();
    }
}
