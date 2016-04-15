using System;
using BLL.Utilities;
using Models.Base;
using SharedDto.Universe.Planets;
using SharedDto.UtilityDto;

namespace BLL.Generation.StarSystem
{
    public static class PlanetProperties
    {
        public const double MinSatelliteDistance = 0.8;
        public const double MaxSatelliteDistance = 2.5;

        /// <summary>
        ///     Determine planet Space Structure
        /// </summary>
        /// <param name="totalSpaces"></param>
        /// <param name="radiationLevel">Planet radiation level</param>
        /// <param name="conditions"></param>
        /// <param name="hasWater">has water</param>
        /// <param name="hasAtmosphere">has atmosphere</param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static Spaces CalculateSpaces(int totalSpaces, int radiationLevel, SystemGenerationDto conditions,
            bool hasWater, bool hasAtmosphere, Random rnd)
        {
            var result = new Spaces();

            if (conditions.ForceLiving)
            {
                ForcedLivingSpaces(result, totalSpaces, conditions.MostlyWater, rnd);
            }
            else
            {
                NormalSpacesCalculation(result, totalSpaces, radiationLevel, conditions.ForceWater,
                    conditions.MostlyWater, hasWater, hasAtmosphere, rnd);
            }

            return result;
        }

        /// <summary>
        ///     This will calculate production for planet
        /// </summary>
        /// <param name="planetDto"></param>
        /// <param name="density"></param>
        /// <param name="earthDensity"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public static Production CalculateProduction(PlanetDto planetDto, double density, double earthDensity,
            SystemGenerationDto conditions)
        {
            var result = new Production
            {
                ActivePopOnFoodProduction = 0,
                ActivePopOnOreProduction = 0,
                ActivePopOnResProduction = 0,
                LastOreUpdateTime = DateTime.Now,
                LastResearchUpdateTime = DateTime.Now,
                LastFoodUpDateTime = DateTime.Now,
                LastMaintenanceUpdateTime = DateTime.Now,
                LastIncomeRevenueTime = DateTime.Now,
                ResearchPointProduction=10
            };

            var baseWaterProduction = 0.24;
            var baseGroundProduction = 0.14;
            var baseMineralProduction = 0.2*density/earthDensity;
            var baseMineralProdOnRad = baseMineralProduction + baseMineralProduction*0.25;

            if (conditions.MineralRich)
            {
                baseMineralProdOnRad += baseMineralProdOnRad*0.5;
                baseMineralProduction += baseMineralProduction*0.5;
            }
            if (conditions.MineralPoor)
            {
                baseMineralProdOnRad -= baseMineralProdOnRad*0.5;
                baseMineralProduction -= baseMineralProduction*0.5;
            }
            if (conditions.FoodRich)
            {
                baseWaterProduction += baseWaterProduction*0.5;
                baseGroundProduction += baseGroundProduction*0.5;
            }
            if (conditions.FoodPoor)
            {
                baseWaterProduction -= baseWaterProduction*0.5;
                baseGroundProduction -= baseGroundProduction*0.5;
            }

            var percWater = planetDto.WaterSpaces/(double) planetDto.Totalspaces;
            var percWaterRad = planetDto.WaterRadiatedSpaces/(double) planetDto.Totalspaces;
            var percGround = planetDto.GroundSpaces/(double) planetDto.Totalspaces;
            var percGroundRad = 100 - percWater - percWaterRad - percGround;

            var foodProd = baseGroundProduction*percGround + baseWaterProduction*percWater;
            var oreProd = baseMineralProduction*percGround + baseMineralProduction*percWater +
                          baseMineralProdOnRad*percGroundRad + baseMineralProdOnRad*percWaterRad;
            result.FoodProduction = (int) foodProd >= 0 ? (int) foodProd : 0;
            result.OreProduction = (int) oreProd >= 0 ? (int) oreProd : 0;
            return result;
        }

        private static void ForcedLivingSpaces(Spaces result, int totalSpaces, bool mostlyWater, Random rnd)
        {
            var percWater = RandomNumbers.RandomDouble(0.10, 0.60, rnd);
            if (mostlyWater)
            {
                percWater = RandomNumbers.RandomDouble(0.6, 0.98, rnd);
            }

            SetSpacesParameters(result, totalSpaces, percWater, 0);
        }

        private static void NormalSpacesCalculation(Spaces result, int totalSpaces, int radiationLevel, bool forceWater,
            bool mostlyWater, bool hasWater, bool hasAtmosphere, Random rnd)
        {
            double percWater = 0;
            if (forceWater || hasWater)
            {
                percWater = mostlyWater
                    ? RandomNumbers.RandomDouble(0.6, 0.98, rnd)
                    : RandomNumbers.RandomDouble(0.10, 0.60, rnd);
            }

            double percRad = 0;
            if (radiationLevel > 3)
            {
                percRad = radiationLevel/10.00;
                if (!hasWater)
                    percRad = percRad + percRad*0.5;
                if (hasAtmosphere) percRad = percRad - percRad*0.5;
                if (percRad > 1) percRad = 1;
            }

            SetSpacesParameters(result, totalSpaces, percWater, percRad);
        }

        private static void SetSpacesParameters(Spaces result, int totalSpaces, double percWater, double percRad)
        {
            var radiatedSpaces = (int) (totalSpaces*percRad);
            var nonradiatedSpaces = totalSpaces - radiatedSpaces;
            result.WaterRadiatedSpaces = (int) (radiatedSpaces*percWater);
            result.WaterSpaces = (int) (nonradiatedSpaces*percWater);
            result.GroundRadiatedSpaces = radiatedSpaces - result.WaterRadiatedSpaces;
            result.GroundSpaces = nonradiatedSpaces - result.WaterSpaces;
            result.GroundUsedSpaces = 0;
            result.WaterUsedSpaces = 0;
        }
    }
}