
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


        //public List<User> Users { get; set; }

        public IList<Invitee> Invitees { get; set; }

        //[NotMapped]
        //public int NumOfInvitees => Invitees.Count();

        public IList<Table> Tables { get; set; }

        //[NotMapped]
        //public int NumOfTables => Tables.Count();

        public IList<Category> Categories { get; set; }

        public IList<EventOption> EventOptions { get; set; }


    }
}
