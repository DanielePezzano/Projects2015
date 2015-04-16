using BLL.Utilities;
using Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Generation.StarSystem
{
    public static class PlanetProperties
    {
        public const double _MinSatelliteDistance = 0.8;
        public const double _MaxSatelliteDistance = 2.5;
        /// <summary>
        /// Determine planet Space Structure
        /// </summary>
        /// <param name="totalSpaces"></param>
        /// <param name="radiationLevel">Planet radiation level</param>
        /// <param name="forceWater">forced water presence</param>
        /// <param name="mostlyWater">planet mostly water coverd</param>
        /// <param name="forceLiving">habitable planet forced</param>
        /// <param name="hasWater">has water</param>
        /// <param name="hasAtmosphere">has atmosphere</param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static Spaces CalculateSpaces(int totalSpaces, int radiationLevel, bool forceWater, bool mostlyWater, bool forceLiving, bool hasWater,bool hasAtmosphere, Random rnd)
        {
            Spaces result = new Spaces();

            if (forceLiving)
            {
               ForcedLivingSpaces(result, totalSpaces, radiationLevel, forceWater, mostlyWater, hasWater, rnd);
            }
            else
            {
                NormalSpacesCalculation(result, totalSpaces, radiationLevel, forceWater, mostlyWater, hasWater, hasAtmosphere, rnd);
            }
            
            return result;
        }
        /// <summary>
        /// This will calculate production for planet
        /// </summary>
        /// <param name="planetSpaces"></param>
        /// <param name="density"></param>
        /// <param name="earthDensity"></param>
        /// <returns></returns>
        public static Production CalculateProduction(Spaces planetSpaces, double density, double earthDensity)
        {
            Production result = new Production();
            result.ActivePopOnFoodProduction = 0;
            result.ActivePopOnOreProduction = 0;
            result.ActivePopOnResProduction = 0;

            double baseWaterProduction = 0.24;
            double baseGroundProduction = 0.14;
            double baseMineralProduction = 0.2 * density / earthDensity;
            double baseMineralProdOnRad = baseMineralProduction + baseMineralProduction * 0.25;

            double percWater = (double)planetSpaces.WaterSpaces/ (double)planetSpaces.Totalspaces;
            double percWaterRad = (double)planetSpaces.WaterRadiatedSpaces / (double)planetSpaces.Totalspaces;
            double percGround = (double)planetSpaces.GroundSpaces / (double)planetSpaces.Totalspaces;
            double percGroundRad = 100 - percWater - percWaterRad - percGround;

            double FoodProd = (baseGroundProduction * percGround) + baseWaterProduction * percWater;
            double OreProd = (baseMineralProduction * percGround) + baseMineralProduction * percWater +
                (baseMineralProdOnRad * percGroundRad) + (baseMineralProdOnRad * percWaterRad);
            result.FoodProduction = (int)FoodProd;
            result.OreProduction = (int)OreProd;
            result.ResearchPointProduction = 10;
            return result;
        }

        private static void ForcedLivingSpaces(Spaces result, int totalSpaces, int radiationLevel, bool forceWater, bool mostlyWater, bool hasWater, Random rnd)
        {
            double percWater = RandomNumbers.RandomDouble(0.10, 0.60, rnd);
            double percRad = 0;
            if (mostlyWater)
            {
                percWater = RandomNumbers.RandomDouble(0.6, 0.98, rnd);
            }

            SetSpacesParameters(result, totalSpaces, percWater, percRad);
        }

        private static void NormalSpacesCalculation(Spaces result, int totalSpaces, int radiationLevel, bool forceWater, bool mostlyWater, bool hasWater, bool hasAtmosphere, Random rnd)
        {
            double percWater = 0;
            if (forceWater || hasWater)
            {
                percWater = (mostlyWater) ? RandomNumbers.RandomDouble(0.6, 0.98, rnd) : RandomNumbers.RandomDouble(0.10, 0.60, rnd);           
            }

            double percRad = 0;
            if (radiationLevel > 3)
            {
                percRad = (double)radiationLevel / 10.00;
                if (!hasWater)
                    percRad = (percRad + percRad * 0.5);
                if (hasAtmosphere) percRad = (percRad - percRad * 0.5);
                if (percRad > 1) percRad = 1;
            }

            SetSpacesParameters(result, totalSpaces, percWater, percRad);
            
        }

        private static void SetSpacesParameters(Spaces result, int totalSpaces, double percWater, double percRad)
        {
            int radiatedSpaces = (int)(totalSpaces * percRad);
            int nonradiatedSpaces = totalSpaces - radiatedSpaces;
            result.WaterRadiatedSpaces = (int)(radiatedSpaces * percWater);
            result.WaterSpaces = (int)(nonradiatedSpaces * percWater);
            result.GroundRadiatedSpaces = radiatedSpaces - result.WaterRadiatedSpaces;
            result.GroundSpaces = nonradiatedSpaces - result.WaterSpaces;
            result.GroudUsedSpaces = 0;
            result.WaterUsedSpaces = 0;
        }
    }
}
