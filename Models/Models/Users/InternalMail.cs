using Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Models.Users
{
    public class InternalMail : BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "MailBody", ResourceType = typeof(Resources))]
        public string Body { get; set; }
        [Required]
        [Display(Name = "Sender", ResourceType = typeof(Resources))]
        public User Sender { get; set; }
        [Required]
        [Display(Name = "Receiver", ResourceType = typeof(Resources))]
        public User Receiver { get; set; }
    }
}
