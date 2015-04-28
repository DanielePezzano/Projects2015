using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SharedDto.Form.Account
{
    [DataContract]
    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "OldPassword", ResourceType = typeof(Resources.ModelResources))]
        [DataMember]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(Resources.ModelResources))]
        [DataMember]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.ModelResources))]
        [DataMember]
        public string ConfirmPassword { get; set; }
    }
}
