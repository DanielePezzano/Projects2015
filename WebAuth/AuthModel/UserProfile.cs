using AuthModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthModel
{
    public class UserProfile :BaseEntity
    {
        [Required()]
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string PasswordChangeToken { get; set; }
        public string ConfirmationToken { get; set; }
        public string UserTableName { get; set; }
        [Required()]
        public string PasswordSalt { get; set; }
        public bool Confirmed { get; set; }       
        public DateTime LastLogin { get; set; }
        public DateTime PasswordChangeRequired { get; set; }
        public DateTime PasswordChangeExpired { get; set; }
        public int LoginError { get; set; }

        public virtual ICollection<Roles> UserRoles { get; set; }
    }
}
