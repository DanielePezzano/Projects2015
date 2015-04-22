using Models.Base;
using Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Universe
{
    [DataContract(IsReference=true)]
    public class Galaxy : BaseEntity
    {
        [Required()]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public virtual ICollection<Star> Stars { get; set; }
        [DataMember]
        public virtual ICollection<User> Users { get; set; }
    }
}