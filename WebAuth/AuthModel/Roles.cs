using AuthModel.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthModel
{
    public class Roles :BaseEntity
    {
        [Required()]
        [StringLength(50,MinimumLength=4)]
        public string Name { get; set; }
        [StringLength(512)]
        public string Description { get; set; }

        public virtual ICollection<UserProfile> Users { get; set; }
    }
}
