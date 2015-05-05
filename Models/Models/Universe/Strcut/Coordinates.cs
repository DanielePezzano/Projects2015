using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Universe.Strcut
{
    [DataContract]
    public struct Coordinates
    {
        [Required] [DataMember] public int X;

        [Required] [DataMember] public int Y;

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}