using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Users.Enum
{
    public enum UserStatus
    {
        [Display(Name = "Active", ResourceType = typeof(Resources))]
        Active,
        [Display(Name = "ToConfirm", ResourceType = typeof(Resources))]
        ToConfirm,
        [Display(Name = "Banned", ResourceType = typeof(Resources))]
        Banned,
        [Display(Name = "Inactive", ResourceType = typeof(Resources))]
        Inactive
    }
}
