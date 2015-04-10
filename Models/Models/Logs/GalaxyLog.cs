using Models.Base;
using Models.Logs.Enum;

namespace Models.Logs
{
    public class GalaxyLog : BaseLogEntity
    {
        public GalaxyLogType LogType { get; set; }
    }
}
