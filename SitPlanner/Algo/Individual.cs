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
        #region data members and constructors

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

        public Individual(Individual copyIndividual)
        {
            this.algoDb = copyIndividual.algoDb;
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

        #endregion

        #region setters and getters
        public Gen[] getGens()
        {
            return gens;
        }

        #endregion

        #region fitness function
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
            fitness -= InviteesPersonalNotSeatTogetherRestrictionPunishment();
            fitness -= InviteesPersonalMustSeatTogetherRestrictionPunishment();


            //invitee-accesabilityRestriction
            fitness -= InviteesAccessabilityRestrictionPunishment();


            if (fitness < 0)
                return 0;
            return fitness;
        }

        #endregion

        #region punishment functions
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

        private int InviteesPersonalNotSeatTogetherRestrictionPunishment()
        {
            int numOfpunished = 0;
            int inviteeTable;
            int inviteeTable2;

            foreach (var personalRestriction in algoDb.personalRestrictions)
            {
                if(personalRestriction.IsSittingTogether == false)
                {
                    inviteeTable = GetInviteeTableIdFromGen(personalRestriction.MainInviteeId);
                    inviteeTable2 = GetInviteeTableIdFromGen(personalRestriction.SecondaryInviteeId);
                    if (inviteeTable == inviteeTable2) {
                        numOfpunished++;
                    }
                }
                
            }
            return numOfpunished * AlgoConsts.punishmentOnCannotSeatTogether;
        }

        private int InviteesPersonalMustSeatTogetherRestrictionPunishment()
        {
            int numOfpunished = 0;
            int inviteeTable;
            int inviteeTable2;

            foreach (var personalRestriction in algoDb.personalRestrictions)
            {
                if (personalRestriction.IsSittingTogether == true)
                {
                    inviteeTable = GetInviteeTableIdFromGen(personalRestriction.MainInviteeId);
                    inviteeTable2 = GetInviteeTableIdFromGen(personalRestriction.SecondaryInviteeId);
                    if (inviteeTable != inviteeTable2)
                    {
                        numOfpunished++;
                    }
                }

            }
            return numOfpunished * AlgoConsts.punishmentOnMustSeatTogether;
        }
        
        private int InviteesAccessabilityRestrictionPunishment()
        {
            int numOfpunished = 0;
            int inviteeTable;
            foreach (var accessibilityRestriction in algoDb.accessibilityRestrictions)
            {
                if (accessibilityRestriction.IsSittingAtTable == false)
                {
                    inviteeTable = GetInviteeTableIdFromGen(accessibilityRestriction.InviteeId);
                    if (inviteeTable == accessibilityRestriction.TableId)
                    {
                        numOfpunished++;
                    }
                }
            }
            return numOfpunished * AlgoConsts.punishmentOnAccessibilityRestriction;
        }

        private int AmountOfInviteesPerTablePunishment()
        {
            int punishment = 0;
            int tableCounter = 0;
            int inviteeExceeded = 0; 
            //for each table from the DB, we will check if the table capacity fit the amount of invitees per table
            foreach (var table in algoDb.tables)
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
            int punishment = 0;

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

        #endregion

        #region  utils

        //random Gen will create Gen with the invitee id by i, and random table
        private Gen generateRandomGen(int i)
        {
            int ran = algoUtils.AlgoRandom(tablesAmount);

            Gen gen = new Gen(invitees[i], tables[ran]);

            return gen;
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

        // Return invitee table id from gens array. if not exist return -1.
        private int GetInviteeTableIdFromGen(int inviteeId)
        {
            foreach (Gen gen in gens)
            {
                if (gen.invitee.Id == inviteeId)
                    return gen.table.Id;
            }
            return -1;
        }

        #endregion
    }
}
