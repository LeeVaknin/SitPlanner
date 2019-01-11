using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class Invitee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PhoneNumber { get; set; }

        public string Address { get; set; }

        public bool IsComing { get; set; }

        public string Comment { get; set; }

        public Table Table { get; set; }

        public List<Category> Category { get; set; }
    }
}
