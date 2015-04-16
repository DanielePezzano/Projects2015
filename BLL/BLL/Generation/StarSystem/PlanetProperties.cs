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

        public static Production CalculateProduction()
        {
            Production result = new Production();
            result.ActivePopOnFoodProduction = 0;
            result.ActivePopOnOreProduction = 0;
            result.ActivePopOnResProduction = 0;
            //result.FoodProduction = 0000;
            //result.OreProduction = 00000;
            //result.ResearchPointProduction = 0000;
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
            if (radiationLevel > 5)
            {
                percRad = (double)radiationLevel / 15.00;
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
