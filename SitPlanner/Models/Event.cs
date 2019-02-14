using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide event name")]
        public string Name { get; set; }

        public List<Arrangement> Arrangements { get; set; }
    }
}
