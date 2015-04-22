using System.Runtime.Serialization;
namespace Models.Buildings.Enums
{
    [DataContract]
    public enum BuildingType
    {
        [EnumMember]
        Civil,
        [EnumMember]
        Military,
        [EnumMember]
        CivilInOrbit,
        [EnumMember]
        MilitaryInOrbit
    }
}