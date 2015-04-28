using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SharedDto.Form.Account
{
    [DataContract]
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Username", ResourceType = typeof(Resources.ModelResources))]
        [DataMember]
        public string UserName { get; set; }

        [Required]
        [Display(Name ="Email", ResourceType=typeof(Resources.ModelResources))]
        [DataType(DataType.EmailAddress)]
        [DataMember]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.ModelResources))]
        [DataMember]
        public string Password { get; set; }

        [Required]
        [DataMember]
        [Display(Name = "UniverseChoice", ResourceType = typeof(Resources.ModelResources))]
        public int GalaxyId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.ModelResources))]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataMember]
        public string ConfirmPassword { get; set; }
    }
}
