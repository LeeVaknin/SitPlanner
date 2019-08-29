using SitPlanner.Models.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Models
{
    public class Table
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please provide table capacity")]
        [Range(0,Int32.MaxValue,ErrorMessage = "Table capacity should be greater than or equal to 1")]
        [Display(Name = "Table capacity")]
        public int CapacityOfPeople { get; set; }

        //[Range(0,CapacityOfPeople,ErrorMessage ="")]
        [Display(Name = "Minimum people around table")]
        public int MinCapacityOfPeople { get; set; }

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

        public enum TableSizesEnum
        {
            Ten,
            Twelve,
            Sixteen
        }

    }
}
