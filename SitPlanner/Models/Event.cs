
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
        public Event() { }
        public Event(string name, DateTime date,IList<Invitee> invitees, IList<Table> tables,
            IList<Category> categories, IList<EventOption> eventOptions)
        {
            this.Name = name;
            this.Date = date;
            this.Invitees = invitees;
            this.Tables = tables;
            this.Categories = categories;
            this.EventOptions = eventOptions;
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide event name")]
        [Display(Name = "Event name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[EventValidate]
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

        public class EventValidate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                DateTime CurrentDate = DateTime.Now;
                string Message = string.Empty;

                if (Convert.ToDateTime(value) < CurrentDate)
                {
                    Message = "Event Date cannot be less than current date";
                    return new ValidationResult(Message);
                }
                return ValidationResult.Success;
            }
        }
    }
}
