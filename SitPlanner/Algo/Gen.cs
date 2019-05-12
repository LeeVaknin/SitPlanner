using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Algo
{
    public class Gen
    {
        public int InviteeId { get; set; }

        public int TableId { get; set; }

        //Gen constructor
        public Gen(int InviteeId, int TableId)
        {
                this.InviteeId = InviteeId;
                this.TableId = TableId;
        }

        public Gen(Gen copyGen)
        {
            this.InviteeId = copyGen.InviteeId;
            this.TableId = copyGen.TableId;
        }

    }
}
