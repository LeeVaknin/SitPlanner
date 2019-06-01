using SitPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Algo
{
    public class Population
    {
        AlgoUtils algoUtils = new AlgoUtils();
        AlgoDb algoDb;
        List<Invitee> invitees;
        List<Table> tables;
        public Individual[] population = new Individual[AlgoConsts.populationLength];
        Individual[] topXIndividuals = new Individual[AlgoConsts.topXAmount];

        public Population(Individual[] individuals, AlgoDb algoDb)
        {
            cloneIndividuals(individuals);
            this.algoDb = algoDb;
        }

        public Population(AlgoDb algoDb)
        {
            this.algoDb = algoDb;
            this.invitees = new List<Invitee>(algoDb.invitees);
            this.tables = new List<Table>(algoDb.tables);
        }

        public void initializePopulation(int size)
        {
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = new Individual(algoDb);
            }
        }

        //Calculate fitness of each individual in single population
        public void CalculateIndividualsFitness()
        {
            for (int i = 0; i < population.Length; i++)
            {
                population[i].CalculateFitness();
            }
        }


        private void cloneIndividuals(Individual[] individuals)
        {
            Individual[] newIndividuals = new Individual[individuals.Length];
            for (int i = 0; i < individuals.Length; i++)
            {
                newIndividuals[i] = new Individual(individuals[i], this.algoDb);
            }
            this.population = newIndividuals;
        }
    }
}
