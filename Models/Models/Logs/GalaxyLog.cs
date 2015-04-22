using Models.Base;
using Models.Logs.Enum;
using System.Runtime.Serialization;

namespace Models.Logs
{
    [DataContract]
    public class GalaxyLog : BaseLogEntity
    {
        [DataMember]
        public GalaxyLogType LogType { get; set; }
    }
}