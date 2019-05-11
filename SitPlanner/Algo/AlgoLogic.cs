using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitPlanner.Models;

namespace SitPlanner.Algo
{
    public class AlgoLogic
    {

        Individual[] topXIndividuals = new Individual[AlgoConsts.topXAmount];
        //SortedList<int, Individual> sortedAllIndividuals = new SortedList<int, Individual>();
        int iterations = 0;
        int iterationsWithNoTopXChange = 0;

        public AlgoLogic()
        {

        }

        public Individual RunAlgo(List<Invitee> invitees, List<Table> tables)
        {
            //Initialize population
            Population pop = new Population(invitees, tables);
            pop.initializePopulation(AlgoConsts.populationLength);


            //Calculate fitness of each individual + update Top X
            pop.CalculateIndividualsFitness();
            updateTopX(pop);

            //While not break condition
            while (!(iterationsWithNoTopXChange > AlgoConsts.NumiterationsWithNoTopXChange || 
                GetIndividualWithBestResult().fitness == AlgoConsts.bestResult || 
                iterations == AlgoConsts.maxIterationsCount))
            {
                //Do selection

                //Do crossover

                //Do mutation under a random probability

                //Calculate new fitness value + update top X
                pop.CalculateIndividualsFitness();
                updateTopX(pop);

                iterations++;
            }

            return GetIndividualWithBestResult();
        }

        private void updateTopX(Population pop)
        {
            int changesCount = 0;
            //go over all individual in a single population and check if its fitness bigger than the Top x
            for (int i = 0; i < AlgoConsts.populationLength; i++)
            {
                for (int x = 0; x < AlgoConsts.topXAmount; x++)
                {
                    if (topXIndividuals[x] != null)
                    {
                        if (pop.population[i].fitness > topXIndividuals[x].fitness)
                        {
                            topXIndividuals[x] = pop.population[i];
                            changesCount++;
                        }
                    }
                    else
                    {
                        topXIndividuals[x] = pop.population[i];
                        changesCount++;
                    }
                    
                }
            }
            //if there was no change - update the iteration flag
            if (changesCount == 0)
                iterationsWithNoTopXChange++;
        }

        private Individual GetIndividualWithBestResult()
        {
            Individual maxIndividual = topXIndividuals[0];

            for (int i = 0; i < AlgoConsts.topXAmount; i++)
            {
                if (topXIndividuals[i].fitness > maxIndividual.fitness)
                    maxIndividual = topXIndividuals[i];
            }
            return maxIndividual;
        }
    }
}
