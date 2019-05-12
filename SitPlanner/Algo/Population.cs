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
        List<Invitee> invitees;
        List<Table> tables;
        public Individual[] population = new Individual[AlgoConsts.populationLength];
        Individual[] topXIndividuals = new Individual[AlgoConsts.topXAmount];

        public Population(Individual[] individuals)
        {
            cloneIndividuals(individuals);
        }

        public Population(List<Invitee> invitees, List<Table> tables)
        {
            this.invitees = new List<Invitee>(invitees);
            this.tables = new List<Table>(tables);
        }

        public void initializePopulation(int size)
        {
            for (int i = 0; i < population.Length; i++)
            {
                population[i] = new Individual(invitees, tables);
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
                newIndividuals[i] = new Individual(individuals[i]);
            }
            this.population = newIndividuals;
        }
    }
}
