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
        public void CalculateFitness()
        {
            int totalPunishment = 0;
            //all invitees exist - MUST
            totalPunishment += InviteesExistensePunishment();

            //limit of amount of invitees per table
            totalPunishment += AmountOfInviteesPerTablePunishment();

            //invitee-category 
            totalPunishment += MultipleCategoriesInTablePunishment();
            totalPunishment += StandaloneInviteePerCategoryPunishment();

            //invitee-restriction (cannot)
            //invitee-restriction (must sit with) 
            totalPunishment += InviteesPersonalRestrictionPunishment();

            //invitee-accesabilityRestriction
            totalPunishment += InviteesAccessabilityRestrictionPunishment();

            if (totalPunishment > this.fitness)
                this.fitness = 0;
            else
                this.fitness -= totalPunishment;
        }

        #endregion

        #region punishment functions
        private int InviteesExistensePunishment()
        {
            int missingInvitee = 0;
            bool isExist = false;

            foreach (var invitee in algoDb.invitees)
            {
                foreach (Gen gen in gens)
                {
                    if (invitee.Id == gen.invitee.Id)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    missingInvitee++;
                    isExist = false;
                }
            }
            return (missingInvitee * AlgoConsts.punishOnMissingInvitee);
        }

        private int InviteesPersonalRestrictionPunishment()
        {
            int totalPunished = 0;
            int notSittingTogetherPunishment = 0;
            int mustSittingTogetherPunishment = 0;
            int inviteeTable;
            int inviteeTable2;

            foreach (var personalRestriction in algoDb.personalRestrictions)
            {
                inviteeTable = GetInviteeTableIdFromGen(personalRestriction.MainInviteeId);
                inviteeTable2 = GetInviteeTableIdFromGen(personalRestriction.SecondaryInviteeId);
                bool sammeTable = (inviteeTable == inviteeTable2);

                if (personalRestriction.IsSittingTogether == false)
                {
                    if (sammeTable)
                        notSittingTogetherPunishment++;
                }
                else
                {
                    if (!sammeTable)
                        mustSittingTogetherPunishment++;
                }
            }
            totalPunished = (notSittingTogetherPunishment * AlgoConsts.punishmentOnCannotSitTogether) + (mustSittingTogetherPunishment * AlgoConsts.punishmentOnMustSitTogether);
            return totalPunished;
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
            List<Invitee> inviteesAroundTable = new List<Invitee>();
            int inviteeExceeded = 0;

            foreach (var table in algoDb.tables)
            {
                inviteesAroundTable = GetInviteesAroundTable(table.Id);
                tableCounter = inviteesAroundTable.Count;

                //punishment for overBooking for a specific table
                if (tableCounter > table.CapacityOfPeople)
                    punishment += inviteeExceeded * AlgoConsts.punishmentOnOverBookingInviteeForTable;
                //punishment for under booking on a table
                else if (tableCounter < table.MinCapacityOfPeople)
                    punishment += Math.Abs(inviteeExceeded) * AlgoConsts.punishmentOnUnderBookingInviteeForTable;
            }
            return punishment;
        }

        private int StandaloneInviteePerCategoryPunishment()
        {
            int numOfSittingAlone = 0;
            foreach (Table table in algoDb.tables)
            {
                List<Invitee> inviteesAroundTable = GetInviteesAroundTable(table.Id);

                foreach (Invitee invitee in inviteesAroundTable)
                {
                    bool isAlone = true;
                    int inviteeCategory = invitee.CategoryId;
                    for (int j = 0; j < inviteesAroundTable.Count; j++)
                    {
                        if (inviteeCategory == inviteesAroundTable[j].CategoryId && invitee.Id != inviteesAroundTable[j].Id)
                        {
                            isAlone = false;
                            break;
                        }
                    }
                    if (isAlone)
                        numOfSittingAlone++;
                }
            }
            return numOfSittingAlone * AlgoConsts.punishmentOnSingleInviteeWithSameCategoryInTable;
        }

        private int MultipleCategoriesInTablePunishment()
        {
            int punishment = 0;

            foreach (var table in algoDb.tables)
            {
                HashSet<int> categories = new HashSet<int>();
                int numberOfCategoriesInTable = 0;
                List<Invitee> inviteesArountTable = GetInviteesAroundTable(table.Id);

                foreach (Invitee inviteeTable in inviteesArountTable)
                {
                    categories.Add(inviteeTable.CategoryId);
                }
                // The number of categories for a specific table
                numberOfCategoriesInTable = categories.Count;
                // Punish on each multi categories which is bigger than 1
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
        // Return invitee tableId, according to the given inviteeId, from gens array. if not exist return -1.
        private int GetInviteeTableIdFromGen(int inviteeId)
        {
            foreach (Gen gen in gens)
            {
                if (gen.invitee.Id == inviteeId)
                    return gen.table.Id;
            }
            return -1;
        }
        // Return list of all invitees id's around the given table
        private List<Invitee> GetInviteesAroundTable(int tableId)
        {
            List<Invitee> inviteesTable = new List<Invitee>();
            foreach (Gen gen in gens)
            {
                if (gen.table.Id == tableId)
                    inviteesTable.Add(gen.invitee);
            }
            return inviteesTable;
        }

        #endregion
    }
}
