using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedDto
{
    [DataContract]
    public class SectorDto
    {
        [DataMember]
        public List<StarDto> Stars { get; set; }
    }
}
