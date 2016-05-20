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
        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        [DataMember]
        public string Name { get; set; }
    }
}