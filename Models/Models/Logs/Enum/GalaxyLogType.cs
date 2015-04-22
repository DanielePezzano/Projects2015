using System.Runtime.Serialization;
namespace Models.Logs.Enum
{
    [DataContract]
    public enum GalaxyLogType
    {
        [EnumMember]
        SystemGenerated,
        [EnumMember]
        PlanetGenerated
    }
}
