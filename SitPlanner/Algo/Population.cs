using SitPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Algo
{
    public class Population
    {
        List<Invitee> invitees;
        List<Table> tables;
        public Individual[] population = new Individual[AlgoConsts.populationLength];
        Individual[] topXIndividuals = new Individual[AlgoConsts.topXAmount];

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

    }
}
