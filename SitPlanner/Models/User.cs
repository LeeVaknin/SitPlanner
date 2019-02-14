using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class User : IdentityUser<int>
    {
        public List<Event> Events { get; set; }
    }
}
