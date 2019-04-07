using SitPlanner.Models.Enums;
using SitPlanner.Models.ManyToMany;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class Invitee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide first name")]
        [Display(Name = "First name")]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please provide last name")]
        [Display(Name = "Last name")]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => String.Join(" ", FirstName, LastName);

        [Required(ErrorMessage = "Please provide phone number")]
        [Display(Name = "Phone number")]
        public int PhoneNumber { get; set; }

        public string Address { get; set; }

        [Display(Name = "Confirmed invatation ?")]
        public bool IsComing { get; set; }

        public string Comment { get; set; }

        // TODO: Configure one-to-many with event
        //public int EventId { get; set; }
        public virtual Event Event { get; set; }

    }
}
