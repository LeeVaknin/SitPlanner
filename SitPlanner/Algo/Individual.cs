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
        AlgoUtils algoUtils = new AlgoUtils();
        public int fitness = -1;
        private List<Invitee> invitees;
        private List<Table> tables;
        private int invitessAmount;
        private int tablesAmount;
        public Gen[] gens;
        AlgoDb algoDb;



        public Individual(int gensSize)
        {
            gens = new Gen[gensSize];
        }

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
            gens = new Gen[invitessAmount - 1];

            //generate gens list - all invitess with random tables
            for (int i = 0; i < gens.Length; i++)
            {
                gens[i] = generateRandomGen(i + 1);
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
            int fitness = 0;

            //per invitee
            //all invitees exist - MUST
            fitness -= InviteesExistensePunishment();

            //invitee-category --> at least 1 with the same category? ++points for more invitees with same category?
            fitness -= InviteesCategoriesPunishment();

            //invitee-restriction (cannot)
            //invitee-restriction (must sit with) 
            fitness -= InviteesPersonalRestrictionPunishment();

            //invitee-accesabilityRestriction
            fitness -= InviteesAccessabilityRestrictionPunishment();

            //limit of amount of invitees per table
            fitness -= AmountOfInviteesPerTablePunishment();

            if (fitness < 0)
                return 0;
            return fitness;
        }

        public Gen[] getGens()
        {
            return gens;
        }

        //random Gen will create Gen with the invitee id by i, and random table
        private Gen generateRandomGen(int i)
        {
            int ran = algoUtils.AlgoRandom(tablesAmount);

            Gen gen = new Gen(invitees[i].Id, tables[ran].Id);

            return gen;
        }

        private int InviteesExistensePunishment()
        {
            int punishment = 0;
            for (int i = 0; i<gens.Length; i++)
            {
                foreach (var invitee in algoDb.invitees)
                {
                    if (!(invitee.Id == gens[i].InviteeId))
                    {
                        punishment -= AlgoConsts.punishOnMissingInvitee; 
                    }
                }
            }
            return punishment;
        }

        //TODO - need to implement
        private int InviteesPersonalRestrictionPunishment()
        {
            int punishment = 0;
            return punishment;
        }

        //TODO - need to implement
        private int InviteesAccessabilityRestrictionPunishment()
        {
            int punishment = 0;
            return punishment;
        }

        //TODO - need to implement
        private int AmountOfInviteesPerTablePunishment()
        {
            int punishment = 0;
            return punishment;
        }

        //TODO - need to implement
        private int InviteesCategoriesPunishment()
        {
            int punishment = 0;
            //for the same table id (god knows how to do that), check if all invitees have same category.

            return punishment;
        }
    }
}
