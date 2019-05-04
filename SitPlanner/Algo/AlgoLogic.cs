using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitPlanner.Models;

namespace SitPlanner.Algo
{
    public class AlgoLogic
    {

        Individual[] top3Individuals = new Individual[3];
        int iterations = 0;
        int iterationsWithNoTop3Change = 0;



        public AlgoLogic(List<Invitee> invitees, List<Table> tables)
        { 

        }

    //Initialize population

    //Calculate fitness of each individual + update Top3

    //While not break condition

        //Do selection

        //Do crossover

        //Do mutation under a random probability

        //Calculate new fitness value + check top3

    }
}
