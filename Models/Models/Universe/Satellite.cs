using Models.Base;
using Models.Universe.Enum;
using System.ComponentModel.DataAnnotations;
using Models.Users;
using System.Collections.Generic;
using Models.Buildings;
using System.Runtime.Serialization;

namespace Models.Universe
{
    [DataContract]
    public class Satellite : BaseSatellite
    {
        [DataMember]
        public virtual Planet Planet { get; set; }
        [DataMember]
        public virtual User User { get; set; }
    }
}