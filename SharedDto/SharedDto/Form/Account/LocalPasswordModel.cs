using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using SharedDto.Resources;

namespace SharedDto.Form.Account
{
    [DataContract]
    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword", ResourceType = typeof(ModelResources))]
        [DataMember]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (ValidationResource), ErrorMessageResourceName = "MinimumLenght", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(ModelResources))]
        [DataMember]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessageResourceType = typeof (ValidationResource), ErrorMessageResourceName = "PasswordsNotMatch")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(ModelResources))]
        [DataMember]
        public string ConfirmPassword { get; set; }
    }
}
