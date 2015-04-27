using System.ComponentModel.DataAnnotations;

namespace SharedDto.Form.Account
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Username", ResourceType = typeof(Resources.ModelResources))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.ModelResources))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(Resources.ModelResources))]
        public bool RememberMe { get; set; }
    }
}
