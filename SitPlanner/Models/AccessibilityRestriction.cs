using SitPlanner.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SitPlanner.Models
{
    public class AccessibilityRestriction
    {
        public int Id { get; set; }

        public int InviteeId { get; set; }
        public virtual Invitee Invitee { get; set; }

        [Display(Name = "Table type")]
        public TableTypeEnum TableType { get; set; }

        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        // true in case invitee can NOT seat at the following table type
        [Display(Name = "The invitee can't seat at this kind of table?")]
        public bool IsSittingAtTable { get; set; }

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
