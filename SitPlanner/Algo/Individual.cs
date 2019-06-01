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
        



        public Individual(int gensSize, AlgoDb algoDb)
        {
            this.algoDb = algoDb;
            gens = new Gen[gensSize];
        }

        public Individual(Individual copyIndividual, AlgoDb algoDb)
        {
            this.algoDb = algoDb;
            cloneGens(copyIndividual.getGens());
            this.invitees = copyIndividual.invitees;
            this.tables = copyIndividual.tables;
        }

        public Individual(AlgoDb algoDb)
        {
            //initialize 
            this.algoDb = algoDb;
            this.invitees = new List<Invitee>(algoDb.invitees);
            this.tables = new List<Table>(algoDb.tables);
            this.invitessAmount = invitees.Count;
            this.tablesAmount = tables.Count;
            gens = new Gen[invitessAmount];

            //generate gens list - all invitess with random tables
            for (int i = 0; i < gens.Length; i++)
            {
                gens[i] = generateRandomGen(i);
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

            //invitee-category 
            fitness -= MultipleCategoriesInTablePunishment();
            fitness -= StandaloneInviteePerCategoryPunishment();

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

            foreach (var invitee in algoDb.invitees)
            {
                for (int i = 0; i < gens.Length; i++)
                {
                    if (invitee.Id == gens[i].invitee.Id)
                    {
                        break;
                    }
                    if (gens.Length-1 == i)
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

        
        private int StandaloneInviteePerCategoryPunishment()
        {
            int punishment = 0;
            //for each invitee in gens, check if there is another invitee from the same table, with the same category. if not - punish
            for (int i = 0; i < gens.Length; i++)
            {
                for (int j = 1; j < gens.Length; j++)
                {
                    if (gens[i].table.Id == gens[j].table.Id)
                        if (gens[i].invitee.CategoryId == gens[j].invitee.CategoryId)
                            break;
                    if (j == gens.Length - 1)
                        punishment++;
                }
            }

            return punishment * AlgoConsts.punishmentOnSingleInviteeWithSameCategoryInTable;
        }

        private int MultipleCategoriesInTablePunishment()
        {
            //for the same table id (god knows how to do that), check if all invitees have same category.
            int punishment = 0;
            //for each table 
            foreach (var table in algoDb.tables)
            {
                HashSet<int> categories = new HashSet<int>();
                int numberOfCategoriesInTable = 0;
                //run on all gens which has the same table, and add the categories into set
                for (int i = 0; i < gens.Length; i++)
                {
                    if (gens[i].table.Id == table.Id)
                        categories.Add(gens[i].invitee.CategoryId);
                }
                //the number of categories for a specific table
                numberOfCategoriesInTable = categories.Count();
                //punish on each multi categories which is bigger than 1
                punishment += (numberOfCategoriesInTable - 1);
            }

            return punishment * AlgoConsts.punishmentOnMultiCategoriesInTable;
        }
    }
}
