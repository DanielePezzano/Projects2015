using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedDto.Form.Universe
{
    [DataContract]
    public class GalaxyList
    {
        [DataMember]
        public List<GalaxyForm> Galaxies { get; set; }
    }
}
