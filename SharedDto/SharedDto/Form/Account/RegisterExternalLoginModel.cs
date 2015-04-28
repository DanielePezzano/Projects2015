using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace SharedDto.Form.Account
{
    [DataContract]
    public class RegisterExternalLoginModel
    {
        [Display(Name = "Username", ResourceType = typeof(Resources.ModelResources))]
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string ExternalLoginData { get; set; }
    }
}
