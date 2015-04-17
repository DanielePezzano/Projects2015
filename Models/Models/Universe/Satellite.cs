using Models.Base;
using Models.Universe.Enum;
using System.ComponentModel.DataAnnotations;
using Models.Users;
using System.Collections.Generic;
using Models.Buildings;

namespace Models.Universe
{
    public class Satellite : BaseSatellite
    {
        public virtual Planet Planet { get; set; }
        public virtual User User { get; set; }
    }
}
