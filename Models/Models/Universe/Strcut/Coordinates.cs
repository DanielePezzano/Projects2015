using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Universe.Strcut
{
    
    public struct Coordinates
    {
        [Required()]
        public int X;
        [Required()]
        public int Y;
    }
}
