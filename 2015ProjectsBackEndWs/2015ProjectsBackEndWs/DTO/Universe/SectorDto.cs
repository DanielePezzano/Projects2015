using System.Collections.Generic;
using System.Runtime.Serialization;

namespace _2015ProjectsBackEndWs.DTO.Universe
{
    [DataContract]
    public class SectorDto
    {
        [DataMember]
        public List<StarDto> Stars { get; set; }
    }
}