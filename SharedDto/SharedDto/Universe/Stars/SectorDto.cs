using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedDto
{
    [DataContract]
    public class SectorDto
    {
        [DataMember]
        public List<StarDto> Stars { get; set; }
    }
}
