using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Hulls;
using Models.Tech;
using System.ComponentModel.DataAnnotations;

namespace Models.Fleets.ShipClasses.Weapons
{
    public class AntiPlanetWeapon : BaseWeaponEntity
    {
        /*
         * Le armi che emettono radiazioni fanno più danni
         * ma rendono contaminati N spazi del pianeta (N numero di turni di bombardamento).
         * Se ci sono spazi liberi sul pianeta >=N -> ridotti di N, aumentati gli spazi contaminati
         * Se ci sono spazi liberi sul pianeta <N  -> Azzerati SpaziLiberi (sl), distrutti Edifici che occupano spazi
         */
        [Display(Name = "RadiationHazard", Description="RadiationHazardHint",ResourceType=typeof(Resources))]
        public bool RadiationHazard { get; set; }

        public virtual Hull Hull { get; set; }
    }
}
