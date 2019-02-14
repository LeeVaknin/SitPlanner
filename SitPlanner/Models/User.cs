using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class User : IdentityUser<int>
    {
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

        public List<Event> Events { get; set; }
    }
}
