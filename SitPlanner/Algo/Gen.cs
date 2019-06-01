using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  SitPlanner.Models;

namespace SitPlanner.Algo
{
    public class Gen
    {
        public Invitee invitee { get; set; }

        public Table table { get; set; }

        //Gen constructor
        public Gen(Invitee invitee, Table table)
        {
                this.invitee = invitee;
                this.table = table;
        }

        public Gen(Gen copyGen)
        {
            this.invitee = copyGen.invitee;
            this.table = copyGen.table;
        }

    }
}
