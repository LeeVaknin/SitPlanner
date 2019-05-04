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
        public int fitness { get; }
        List<Invitee> invitees;
        List<Table> tables;
        private int invitessAmount;
        private int tablesAmount;
        Gen[] gens;

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
        //calculate individual fitness
        public int CalculateFintess()
        {

            return fitness;
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
