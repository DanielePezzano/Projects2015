
using System.Runtime.Serialization;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;

namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class AntiShipWeaponDto : BaseDto, IDto, ICosts, ISpaces
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int RateOfFire { get; set; } //quante volte spara in un round
        [DataMember]
        public int Damage { get; set; }
        [DataMember]
        public int BonusToHit { get; set; }
        [DataMember]
        public bool IsBeamWeapon { get; set; } //le armi a raggi Ignorano l'armatura ma vengono bloccate dagli scudi
        /*
         * //le flotte cominciano la battaglia a distanza 100, le armi con maggior raggio d'azione sparano prima 
         * quando ovviamente è il turno della nave su cui sono equipaggiate:
         * 1) All'inizio della battaglia si definisce l'iniziativa, d20+velocità delle classi di navi
         * 2) le navi con iniziativa migliore agiscono per prime
         * 3) la classe di navi a cui spetta l'iniziativa, controlla che qualche arma possa raggiungere una nave nemica
         * 4) le armi che possono raggiungere le navi nemiche, attaccano e fanno danni.
         */
        [DataMember]
        public int RayOfFire { get; set; }
        [DataMember]
        public int OreCost { get; set; }
        [DataMember]
        public int MoneyCost { get; set; }
        [DataMember]
        public int OreMaintenanceCost { get; set; }
        [DataMember]
        public int MoneyMaintenanceCost { get; set; }
        [DataMember]
        public int SpacesNeeded { get; set; }
    }
}
