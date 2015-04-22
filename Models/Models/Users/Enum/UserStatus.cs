using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models.Users.Enum
{
    [DataContract]
    public enum UserStatus
    {
        [EnumMember]
        [Display(Name = "Active", ResourceType = typeof(Resources))]
        Active,
        [EnumMember]
        [Display(Name = "ToConfirm", ResourceType = typeof(Resources))]
        ToConfirm,
        [EnumMember]
        [Display(Name = "Banned", ResourceType = typeof(Resources))]
        Banned,
        [EnumMember]
        [Display(Name = "Inactive", ResourceType = typeof(Resources))]
        Inactive
    }
}