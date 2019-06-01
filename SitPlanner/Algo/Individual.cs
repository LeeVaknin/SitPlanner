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
        public int fitness = AlgoConsts.fitnessBestResult;
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

            //all invitees exist - MUST
            fitness -= InviteesExistensePunishment();

            //limit of amount of invitees per table
            fitness -= AmountOfInviteesPerTablePunishment();

            //invitee-category --> at least 1 with the same category? ++points for more invitees with same category?
            fitness -= InviteesCategoriesPunishment();

            //invitee-restriction (cannot)
            //invitee-restriction (must sit with) 
            fitness -= InviteesPersonalRestrictionPunishment();

            //invitee-accesabilityRestriction
            fitness -= InviteesAccessabilityRestrictionPunishment();

            

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

            Gen gen = new Gen(invitees[i], tables[ran]);

            return gen;
        }

        private int InviteesExistensePunishment()
        {
            int punishment = 0;
            int missingInvitee = 0; 

            foreach (var invitee in invitees)
            {
                for (int i = 0; i < gens.Length; i++)
                {
                    if (invitee == gens[i].invitee)
                    {
                        break;
                    }
                    if (gens[gens.Length-1].invitee != invitee)
                        missingInvitee++; 
                } 
            }
            return (punishment = missingInvitee * AlgoConsts.punishOnMissingInvitee);
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

        
        private int AmountOfInviteesPerTablePunishment()
        {
            int punishment = 0;
            int tableCounter = 0;
            int inviteeExceeded = 0; 
            //for each table from the DB, we will check if the table capacity fit the amount of invitees per table
            foreach (var table in tables)
            {
                inviteeExceeded = 0;
                tableCounter = 0;
                //count the amount of invitees per table in Individual (gens[]) 
                for (int i = 0; i < gens.Length; i++)
                {
                    if (table.Id == gens[i].table.Id)
                        tableCounter++;
                }
                
                //if the amount of of invtees per table > capacity, than the inviteeEcceeded > 0
                inviteeExceeded = tableCounter - table.CapacityOfPeople;

                //punishment for overBooking for a specific table, punishment will increase
                if (inviteeExceeded > 0)
                    punishment += inviteeExceeded * AlgoConsts.punishmentOnOverBookingInviteeForTable;
                //punishment for under booking on a table . the punishment will increase 
                else
                    punishment += Math.Abs(inviteeExceeded) * AlgoConsts.punishmentOnUnderBookingInviteeForTable;
            }

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
