using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitPlanner.Models;
using SitPlanner.Algo;

namespace SitPlanner.Algo
{
    public class AlgoLogic
    {
        #region data members and constructors

        AlgoUtils algoUtils = new AlgoUtils();
        AlgoDb algoDb;
        Individual[] topXIndividuals = new Individual[AlgoConsts.topXAmount];
        int iterations = 0;
        int iterationsWithoutTopXChange = 0;
        private List<Individual[]> parentsCouplesList = new List<Individual[]>();

        public static int GlobalEventID;
        public int eventIDGlobal { get; set; }

        public AlgoLogic()
        {

        }

        #endregion
        public Individual RunAlgo(AlgoDb algoDb)
        {
            this.algoDb = algoDb;
            //Initialize population
            Population population = new Population(algoDb);
            population.initializePopulation(AlgoConsts.populationLength);

            //Calculate fitness of each individual + update Top X
            population.CalculateIndividualsFitness();
            updateTopX(population);

            //While not break condition
            while (!breakCondition())             
            {
                //Do selection
                parentsCouplesList = Selection(population);

                //TODO - population includes individual which doesnt include algoDB! to fix! 
                //Do crossover - get list of paretns - return new pointer for population
                population = CrossOver(parentsCouplesList);

                //Do mutation under a random probability
                if (algoUtils.AlgoRandom(1000) < 5)
                {
                    population.runMutation();
                }

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

            return (iterationsWithoutTopXChange > AlgoConsts.numIterationsWithoutChange ||
            GetIndividualWithBestResult().fitness == AlgoConsts.optimalResult ||
            iterations == AlgoConsts.maxIterationsCount);
        }

        private List<Individual[]> Selection(Population population)
        {            
            List<Individual[]> parentsList = new List<Individual[]>();

            for (int i = 0; i < population.population.Length ; i = i+2)
            {
                parentsList.Add(getCoupleParents(population));
            }

            return parentsList;
        }

        private Population CrossOver(List<Individual[]> parentsCouplesList)
        {
            int childCount = 0;
            //new children array for new population
            Individual[] newChildrenArray = new Individual[AlgoConsts.populationLength];

            //for each couple of parents
            foreach (var parentsCouple in parentsCouplesList)
            {
                //couple of children - holder for cross over return value
                Individual[] coupleChildren = new Individual[2];

                //create 2 new children with Cross Over
                coupleChildren = Create2ChildrenFrom2ParentsWithCrossOver(parentsCouple);

                //insert to the Individual final array, the new children couple
                for (int i = 0; i < 2; i++)
                {
                    childCount++;
                    newChildrenArray[childCount-1] = coupleChildren[i];
                }
            }
            //why new?? might be the reson for the missing data!!!!!
            return new Population(newChildrenArray, this.algoDb);
        }

        private Individual[] Create2ChildrenFrom2ParentsWithCrossOver(Individual[] individuals)
        {
            //initial children array
            Individual[] children = new Individual[2];
            int gensAmount = individuals[0].getGens().Length;
            children[0] = new Individual(gensAmount, this.algoDb);
            children[1] = new Individual(gensAmount, this.algoDb);

            //on each child copy parent half gens into a child
            children[0].updateGensByIndex(individuals[0].getGens(), 0, gensAmount / 2);
            children[0].updateGensByIndex(individuals[1].getGens(), gensAmount / 2, gensAmount);
            children[1].updateGensByIndex(individuals[1].getGens(), 0, gensAmount / 2);
            children[1].updateGensByIndex(individuals[0].getGens(), gensAmount / 2, gensAmount);

            return children;
        }

        private Individual findlIndividualByRandom(int random, Population population)
        {
            int localSum = 0;

            for (int i = 0; i < population.population.Length; i++)
            {
                localSum += population.population[i].fitness;
                if (localSum >= random)
                {
                    return population.population[i];
                }
            }
            return population.population[population.population.Length-1];
        }

        private Individual[] getCoupleParents(Population population)
        {
            int sumFitness = 0;
            for (int i = 0; i < population.population.Length; i++)
            {
                sumFitness += population.population[i].fitness;
            }

            Individual[] parents = new Individual[2];
            parents[0] = findlIndividualByRandom(algoUtils.AlgoRandom(sumFitness+1), population);
            parents[1] = findlIndividualByRandom(algoUtils.AlgoRandom(sumFitness+1), population);

            return parents;
        }
    }
}
