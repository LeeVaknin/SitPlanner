
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SitPlanner.Models
{
    public class EventOption
    {

        public EventOption(Event @event)
        {
            this.Event = @event;
        }

        public EventOption()
        {

        }

        public EventOption([Bind("Id,EventId")] EventOption eventOption)
        { }
        public int Id { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public List<PersonalRestriction> PersonalRestrictions { get; set; }

        public List<AccessibilityRestriction> AccessibilityRestrictions { get; set; }

    }
}
