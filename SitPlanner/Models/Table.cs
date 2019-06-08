using SitPlanner.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class Table
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide table capacity")]
        [Display(Name = "Table capacity")]
        public int CapacityOfPeople { get; set; }

        // TODO: how to impliment - hard coded? or let the user decide on each table?
        //public int MinCapacityOfPeople => CapacityOfPeople - 2;

        //TODO: ************I put it in comment to avoid the error- Temporary*********
       // public int MinCapacityOfPeople { get; set; }

        [Display(Name = "Table type")]
        public TableTypeEnum TableType { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public enum TableTypeEnum
        {
            High,
            Low,
            Square,
            Round,
            Rectangle
        }

    }
}
