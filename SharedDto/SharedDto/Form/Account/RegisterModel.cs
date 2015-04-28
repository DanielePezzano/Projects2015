using SharedDto.Resources;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SharedDto.Form.Account
{
    [DataContract]
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Username", ResourceType = typeof(ModelResources))]
        [DataMember]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email", ResourceType = typeof(ModelResources))]
        [DataType(DataType.EmailAddress,ErrorMessageResourceType=typeof(ValidationResource), ErrorMessageResourceName="NotValidEmail")]
        [DataMember]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "StringLenght", ErrorMessageResourceType = typeof(ValidationResource), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(ModelResources))]
        [DataMember]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(ValidationResource), ErrorMessageResourceName = "RequiredUniverse")]
        [DataMember]
        [Display(Name = "UniverseChoice", ResourceType = typeof(ModelResources))]
        public int GalaxyId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(ModelResources))]
        [Compare("Password", ErrorMessageResourceName = "PasswordNotMatch", ErrorMessageResourceType = typeof(ValidationResource))]
        [DataMember]
        public string ConfirmPassword { get; set; }
    }
}
