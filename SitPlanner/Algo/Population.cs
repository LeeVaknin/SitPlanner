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
        Individual[] population = new Individual[AlgoConsts.populationLength];
        public Individual firstMaxFit { get; set; }
        public Individual secondMaxFit { get; set; }
        public Individual thirdMaxFit { get; set; }

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

        //Calculate fitness of each individual
        public void CalculateTop3Fintess()
        {

            for (int i = 0; i < population.Length; i++)
            {
                population[i].CalculateFintess();
            }
            firstMaxFit = GetFittest();
            secondMaxFit = GetFittest(firstMaxFit.fitness);
            thirdMaxFit = GetFittest(secondMaxFit.fitness);
        }

        //Get the fittest individual in a population
        public Individual GetFittest(int currentMaxfit = -1)
        {
            int maxFit = currentMaxfit;
            int maxFitIndex = -1;
            for (int i = 0; i < population.Length; i++)
            {
                if (maxFit <= population[i].fitness)
                {
                    maxFit = population[i].fitness;
                    maxFitIndex = i;
                }
            }
            return population[maxFitIndex];
        }
    }
}
