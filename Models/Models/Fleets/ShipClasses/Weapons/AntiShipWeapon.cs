using Models.Fleets.ShipClasses.Base;
using Models.Fleets.ShipClasses.Hulls;
using Models.Tech;
using System.ComponentModel.DataAnnotations;

namespace Models.Fleets.ShipClasses.Weapons
{
    public class AntiShipWeapon : BaseWeaponEntity
    {
        [Display(Name="BeamWeapon",ResourceType=typeof(Resources))]
        public bool IsBeamWeapon { get; set; } //le armi a raggi Ignorano l'armatura ma vengono bloccate dagli scudi
        /*
         * //le flotte cominciano la battaglia a distanza 100, le armi con maggior raggio d'azione sparano prima 
         * quando ovviamente è il turno della nave su cui sono equipaggiate:
         * 1) All'inizio della battaglia si definisce l'iniziativa, d20+velocità delle classi di navi
         * 2) le navi con iniziativa migliore agiscono per prime
         * 3) la classe di navi a cui spetta l'iniziativa, controlla che qualche arma possa raggiungere una nave nemica
         * 4) le armi che possono raggiungere le navi nemiche, attaccano e fanno danni.
         */
        [Display(Name = "RayOfFire", Description="RayOfFireHint", ResourceType = typeof(Resources))]
        public int RayOfFire { get; set; }

        public virtual Hull Hull { get; set; }
    }
}
