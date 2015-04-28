using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SharedDto.Form.Account
{
    [DataContract]
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username", ResourceType = typeof(Resources.ModelResources))]
        [DataMember]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.ModelResources))]
        [DataMember]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.ModelResources))]
        [DataMember]
        public bool RememberMe { get; set; }
    }
}
