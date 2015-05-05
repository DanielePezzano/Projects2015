using System.Runtime.Serialization;

namespace Models.Tech.Enum
{
    [DataContract]
    public enum BonusType
    {
        [EnumMember] Attackbonus,
        [EnumMember] Defencebonus,
        [EnumMember] Rofbonus,
        [EnumMember] Speedbonus,
        [EnumMember] Energybonus,
        [EnumMember] Researchbonus,
        [EnumMember] Populationbonus,
        [EnumMember] Taxbonus,
        [EnumMember] Socialbonus,
        [EnumMember] Armorbonus,
        [EnumMember] Foodbonus,
        [EnumMember] Planettypeallowed,
        [EnumMember] Range,
        [EnumMember] ScannerRange,
        [EnumMember] ShieldBonus,
        [EnumMember] OreBonus,
        [EnumMember] GoldBonus,
        [EnumMember] MaintBonus,
        [EnumMember] Space,
        [EnumMember] Armopiercingfactor,
        [EnumMember] Terraformfactor,
        [EnumMember] Spatialcompressionfactor,
        [EnumMember] Battlespeedbonus,
        [EnumMember] Prerequisito,
        [EnumMember] Bonustimetobuild,
        [EnumMember] Spyingbonus,
        [EnumMember] Precisionbonus
    }
}