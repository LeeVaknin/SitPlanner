using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class Category
    {
        public Category() { }

        public Category(string name, Event @event)
        {
            this.Name = name;
            this.Event = @event;
        }
        public Category([Bind("Name, Event")] Category category) { }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide category name")]
        [Display(Name = "Category name")]
        public string Name { get; set; }

        public int EventId { get; set; }

        [Required(ErrorMessage = "Please choose event")]
        [Display(Name = "Event name")]
        public virtual Event Event { get; set; }

    }
}
