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
            saveTop3(pop);

            //While not break condition
            while (iterationsWithNoTop3Change < AlgoConsts.iterationsWithNoTop3Change)
            {
                //Do selection

                //Do crossover

                //Do mutation under a random probability

                //Calculate new fitness value + check top3
                pop.CalculateTop3Fintess();
                saveTop3(pop);

            }

            bestResult = pop.firstMaxFit;
            return bestResult; 
        }

        private void saveTop3(Population pop)
        {
            int top3Change = 0;
            for (int i = 0; i < top3Individuals.Length; i++)
            {
                if (top3Individuals[i] == null || pop.firstMaxFit.fitness < top3Individuals[i].fitness)
                {
                    top3Individuals[i] = pop.firstMaxFit;
                    top3Change++;
                    break;
                }
            }
            for (int i = 0; i < top3Individuals.Length; i++)
            {
                if (top3Individuals[i] == null || pop.secondMaxFit.fitness < top3Individuals[i].fitness)
                {
                    top3Individuals[i] = pop.secondMaxFit;
                    top3Change++;
                    break;
                }
            }
            for (int i = 0; i < top3Individuals.Length; i++)
            {
                if (top3Individuals[i] == null || pop.thirdMaxFit.fitness < top3Individuals[i].fitness)
                {
                    top3Individuals[i] = pop.thirdMaxFit;
                    top3Change++;
                    break;
                }
            } 

            if (!(top3Change > 0))
                iterationsWithNoTop3Change++;
        }
    }
}
