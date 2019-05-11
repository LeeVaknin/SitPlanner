using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitPlanner.Algo;
using SitPlanner.Models;

namespace SitPlanner.Algo
{
    public class Individual
    {
        public int fitness = -1;
        private List<Invitee> invitees;
        private List<Table> tables;
        private int invitessAmount;
        private int tablesAmount;
        public Gen[] gens;


       
        public Individual(Individual copyIndividual)
        {
            cloneGens(copyIndividual.getGens());
        }

        public Individual(Gen[] gens)
        {
            cloneGens(gens);
        }

        public Individual(List<Invitee> invitees, List<Table> tables)
        {
            //initialize 
            this.invitees = new List<Invitee>(invitees);
            this.tables = new List<Table>(tables);
            this.invitessAmount = invitees.Count;
            this.tablesAmount = tables.Count;
            gens = new Gen[invitessAmount-1];

            //generate gens list - all invitess with random tables
            for (int i = 0; i < gens.Length; i++)
            {
                gens[i] = generateRandomGen(i+1);
            }
        }
       
        public void cloneGens(Gen[] gens)
        {
            Gen[] newGens = new Gen[gens.Length];
            for (int i = 0; i < gens.Length; i++)
            {
                newGens[i] = new Gen(gens[i]);
            }

            this.gens = newGens;
        }

        public void updateGensByIndex(Gen[] gens, int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                this.gens[i] = gens[i];
            }
            
        }

        //calculate individual fitness
        public int CalculateFitness()
        {
            //per invitee
                //all invitees exist - MUST
                //invitee-category --> at least 1 with the same category? ++points for more invitees with same category?
                //invitee-restriction (cannot)
                //invitee-restriction (must sit with) 
                //invitee-accesabilityRestriction
           
            //per table
                //limit of amount of invitees per table
                //category - all invitees in the table are with the same category? 
            return fitness;
        }

        public Gen[] getGens()
        {
            return gens;
        }

        //random Gen will create Gen with the invitee id by i, and random table
        private Gen generateRandomGen(int i)
        {
            Random rn = new Random();
            int ran = Convert.ToInt32(Math.Abs(rn.NextDouble())) % tablesAmount;

            Gen gen = new Gen(invitees[i].Id, tables[ran].Id);

            return gen;
        }

       
    }
}
