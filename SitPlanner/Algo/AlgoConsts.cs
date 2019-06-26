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
        public const int punishmentOnOverBookingInviteeForTable = 6000;
        public const int punishmentOnUnderBookingInviteeForTable = 700;
        public const int punishmentOnMultiCategoriesInTable = 400;
        public const int punishmentOnCannotSitTogether = 200;
        public const int punishmentOnMustSitTogether = 200;
        public const int punishmentOnAccessibilityRestriction = 100;
        public const int punishmentOnSingleInviteeWithSameCategoryInTable = 800;

        #endregion
    }
}
