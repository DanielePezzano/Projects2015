using System.ComponentModel.DataAnnotations;

namespace SharedDto.Form.Account
{
    public class RegisterExternalLoginModel
    {
        [Display(Name = "Username", ResourceType = typeof(Resources.ModelResources))]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }
}
