using SitPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SitPlanner.Algo
{
    public class Population
    {
        #region data members and construcror
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
        #endregion
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
        //    List<Thread> threads = new List<Thread>();
        //    for (int i = 0; i < population.Length; i++)
        //    {
        //        Thread t = new Thread(() => population[i].CalculateFitness());
        //        t.Start();
        //        threads.Add(t);
        //        //population[i].CalculateFitness();
        //    }
        //    foreach (Thread thread in threads)
        //    {
        //        thread.Join();
        //    }
        //}

            for (int i = 0; i<population.Length; i++)
            {
                population[i].CalculateFitness();
                if (population[i].fitness == Algo.AlgoConsts.fitnessBestResult)
                {

                }
                else if (population[i].fitness > Algo.AlgoConsts.fitnessBestResult - 2000)
                {
                    
                }
                else if (population[i].fitness > Algo.AlgoConsts.fitnessBestResult - 3000)
                {

                }
            }
}

public void runMutation()
        {
            if(this.population.Length <= 0)
            {

            }
            int randomeIndividualId = algoUtils.AlgoRandom(this.population.Length);
            int randomeIndividualId2 = algoUtils.AlgoRandom(this.population.Length);
            if (randomeIndividualId <= 0)
            {

            }
            int randomeGenId = algoUtils.AlgoRandom(this.population[randomeIndividualId].gens.Length);
            int randomeGenId2 = algoUtils.AlgoRandom(this.population[randomeIndividualId].gens.Length);
            if (algoDb.tables.Count <= 0)
            {
                
            }
            int randomeTableId = algoUtils.AlgoRandom(algoDb.tables.Count);
            int randomeTableId2 = algoUtils.AlgoRandom(algoDb.tables.Count);
            Invitee genInvitee = this.population[randomeIndividualId].gens[randomeGenId].invitee;
            Invitee genInvitee2 = this.population[randomeIndividualId2].gens[randomeGenId2].invitee;
            this.population[randomeIndividualId].gens[randomeGenId] = new Gen(genInvitee, algoDb.tables[randomeTableId]);
            this.population[randomeIndividualId2].gens[randomeGenId2] = new Gen(genInvitee2, algoDb.tables[randomeTableId2]);
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
