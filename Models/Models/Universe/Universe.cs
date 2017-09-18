using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using BaseModels;

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