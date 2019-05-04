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
        Individual bestResult;



        public AlgoLogic()
        { 

        }

        public Individual RunAlgo(List<Invitee> invitees, List<Table> tables)
        {
            //Initialize population
            Population pop = new Population(invitees, tables);
            pop.initializePopulation(AlgoConsts.populationLength);


            //Calculate fitness of each individual + update Top3
            pop.CalculateTop3Fintess();

            
            //While not break condition
            while (false)
            {
                //Do selection
                
                //Do crossover

                //Do mutation under a random probability

                //Calculate new fitness value + check top3

            }

            return bestResult; 
        }

    }
}
