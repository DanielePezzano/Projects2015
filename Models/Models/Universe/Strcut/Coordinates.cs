using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Universe.Strcut
{
    [DataContract]
    public struct Coordinates
    {
        [Required()]
        [DataMember]
        public int X;
        [Required()]
        [DataMember]
        public int Y;

        public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
