using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Algo
{
    public class AlgoConsts
    {
        #region Population

        public const int populationLength = 10;
        public const int topXAmount = 3;

        #endregion

        #region Algo

        public const int numIterationsWithoutChange = 1000;
        public const int optimalResult = fitnessBestResult;
        public const int maxIterationsCount = 100;

        #endregion

        #region Fitness
        public const int fitnessWorstResult = 0;
        public const int fitnessBestResult = 10000;
        public const int punishOnMissingInvitee = 1000;
        public const int punishmentOnOverBookingInviteeForTable = 1000;
        public const int punishmentOnUnderBookingInviteeForTable = 100;
        public const int punishmentOnMultiCategoriesInTable = 100;
        public const int punishmentOnCannotSeatTogether = 100;
        public const int punishmentOnMustSeatTogether = 100;
        public const int punishmentOnSingleInviteeWithSameCategoryInTable = 500;

        #endregion
    }
}
