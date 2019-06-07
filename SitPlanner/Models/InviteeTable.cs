using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace SitPlanner.Models
{
    public class InviteeTable
    {

        public InviteeTable()
        {

        }

        public InviteeTable(Invitee invitee, Table table, EventOption eventOption, Event @event)
        {
            this.Invitee = invitee;
            this.Table = table;
            this.EventOption = eventOption;
            this.Event = @event;
        }

        public InviteeTable(Invitee invitee, Table table)
        {
            this.Invitee = invitee;
            this.Table = table;
            this.EventOption = null;
            this.Event = null;
        }

        public InviteeTable([Bind("Id,InviteeId,TableId,EventOptionId,EventId")] InviteeTable inviteeTable)
        //Invitee invitee, Table table, 
        //EventOption eventOption, Event @event)
        {
            //this.Invitee = invitee;
            //this.Table = table;
            //this.EventOption = eventOption;
            //this.Event = @event;
        }

        public int Id { get; set; }

        public int InviteeId { get; set; }
        public virtual Invitee Invitee { get; set; }

        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        public int EventOptionId { get; set; }
        public virtual EventOption EventOption { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

    }
}
