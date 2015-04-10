using Models.Base;
using Models.Logs.Enum;
using Models.Users;
using System;

namespace Models.Logs
{
    public class UserLog : BaseLogEntity
    {
        public UserLogType LogType { get; set; }
        public virtual User User { get; set; }
    }
}
