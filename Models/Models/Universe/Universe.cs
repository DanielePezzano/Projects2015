using Models.Base;
using Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Universe
{
    public class Galaxy : BaseEntity, Models.Universe.IGalaxy
    {
        [Required()]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        public string Name { get; set; }

        public virtual ICollection<Star> Stars { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
