using Models.Base;
using Models.Logs.Enum;
using Models.Users;
using System;
using System.Runtime.Serialization;

namespace Models.Logs
{
    [DataContract(IsReference=true)]
    public class UserLog : BaseLogEntity
    {
        [DataMember]
        public UserLogType LogType { get; set; }
        [DataMember]
        public virtual User User { get; set; }
    }
}