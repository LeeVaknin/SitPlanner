﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitPlanner.Algo
{
    public class AlgoConsts
    {
        #region Population

        public const int populationLength = 150;
        public const int topXAmount = 3;

        #endregion

        #region Algo

        public const int numIterationsWithoutChange = 1000;
        public const int optimalResult = fitnessBestResult;
        public const int maxIterationsCount = 1000;

        #endregion

        #region Fitness
        public const int fitnessWorstResult = 0;
        public const int fitnessBestResult = 99999999;
        public const int punishOnMissingInvitee = 100;
        public const int punishmentOnOverBookingInviteeForTable = 900;
        public const int punishmentOnUnderBookingInviteeForTable = 10;
        public const int punishmentOnMultiCategoriesInTable = 950;
        public const int punishmentOnSingleInviteeWithSameCategoryInTable = 900;
        public const int punishmentOnCannotSitTogether = 12;
        public const int punishmentOnMustSitTogether = 12;
        public const int punishmentOnAccessibilityRestriction = 2;
        public const int punishmentOnIsComming = 2;

        #endregion
    }
}
