using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Algo
{
    public class AlgoConsts
    {
        #region Population

        public const int populationLength = 100;
        public const int topXAmount = 3;

        #endregion

        #region Algo

        public const int numIterationsWithoutChange = 1000;
        public const int optimalResult = fitnessBestResult;
        public const int maxIterationsCount = 10000;

        #endregion

        #region Fitness
        public const int fitnessWorstResult = 0;
        public const int fitnessBestResult = 100000;
        public const int punishOnMissingInvitee = 10000;
        public const int punishmentOnOverBookingInviteeForTable = 15000;
        public const int punishmentOnUnderBookingInviteeForTable = 1000;
        public const int punishmentOnMultiCategoriesInTable = 30000;
        public const int punishmentOnSingleInviteeWithSameCategoryInTable = 20000;
        public const int punishmentOnCannotSitTogether = 1200;
        public const int punishmentOnMustSitTogether = 1200;
        public const int punishmentOnAccessibilityRestriction = 200;

        #endregion
    }
}
