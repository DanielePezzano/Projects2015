using Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Users
{
    [DataContract(IsReference=true)]
    public class InternalMail : BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "MailBody", ResourceType = typeof(Resources))]
        [DataMember]
        public string Body { get; set; }
        [Required]
        [Display(Name = "Sender", ResourceType = typeof(Resources))]
        [DataMember]
        public User Sender { get; set; }
        [Required]
        [Display(Name = "Receiver", ResourceType = typeof(Resources))]
        [DataMember]
        public int Receiver { get; set; }
    }
}
