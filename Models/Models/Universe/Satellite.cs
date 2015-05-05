using System.Runtime.Serialization;
using Models.Base;
using Models.Users;

namespace Models.Universe
{
    [DataContract(IsReference = true)]
    public class Satellite : BaseSatellite
    {
        [DataMember]
        public virtual Planet Planet { get; set; }

        [DataMember]
        public virtual User User { get; set; }
    }
}