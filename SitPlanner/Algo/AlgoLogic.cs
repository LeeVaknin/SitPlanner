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
        int iterations = 0;
        int iterationsWithoutTopXChange = 0;

        public AlgoLogic()
        {

        }

        public Individual RunAlgo(List<Invitee> invitees, List<Table> tables)
        {
            //Initialize population
            Population population = new Population(invitees, tables);
            population.initializePopulation(AlgoConsts.populationLength);


            //Calculate fitness of each individual + update Top X
            population.CalculateIndividualsFitness();
            updateTopX(population);

            
            //While not break condition
            while (!breakCondition())
                
            {
                //Do selection


                //Do crossover


                //Do mutation under a random probability

                //Calculate new fitness value + update top X
                population.CalculateIndividualsFitness();
                updateTopX(population);

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
                iterationsWithoutTopXChange++;
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

        private bool breakCondition()
        {

            return (iterationsWithoutTopXChange > AlgoConsts.NumIterationsWithoutChange ||
            GetIndividualWithBestResult().fitness == AlgoConsts.optimalResult ||
            iterations == AlgoConsts.maxIterationsCount);
        }
    }
}
