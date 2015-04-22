using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Tech.Enum
{
    [DataContract]
    public enum TechnologyField
    {
        [Display(Name = "Buildings", ResourceType = typeof(Resources))]
        [EnumMember]
        Buildings,      //
        [Display(Name = "Social", ResourceType = typeof(Resources))]
        [EnumMember]
        Social,         //
        [Display(Name = "Phisycs", ResourceType = typeof(Resources))]
        [EnumMember]
        Phisycs,        //
        [Display(Name = "ShipComponent", ResourceType = typeof(Resources))]
        [EnumMember]
        ShipComponent,  //
        [Display(Name = "ShipFrame", ResourceType = typeof(Resources))]
        [EnumMember]
        ShipFrame,      //
        [Display(Name = "Weapons", ResourceType = typeof(Resources))]
        [EnumMember]
        Weapons,        //
        [Display(Name = "Enviroment", ResourceType = typeof(Resources))]
        [EnumMember]
        Enviroment,     //
        [Display(Name = "Mathematics", ResourceType = typeof(Resources))]
        [EnumMember]
        Mathematics,
    }
    [DataContract]
    public enum TechnologySubField
    {
        #region Shipcomponent
        [Display(Name = "Engine", ResourceType = typeof(Resources))]
        [EnumMember]
        Engine,
        [Display(Name = "Armor", ResourceType = typeof(Resources))]
        [EnumMember]
        Armor,
        [Display(Name = "Shield", ResourceType = typeof(Resources))]
        [EnumMember]
        Shield,
        [Display(Name = "Tools", ResourceType = typeof(Resources))]
        [EnumMember]
        Tools,
        #endregion
        #region ShipFrame
        [Display(Name = "Frame", ResourceType = typeof(Resources))]
        [EnumMember]
        Frame,
        #endregion
        #region Weapons
        [Display(Name = "Bombs", ResourceType = typeof(Resources))]
        [EnumMember]
        Bombs,
        [Display(Name = "Beams", ResourceType = typeof(Resources))]
        [EnumMember]
        Beams,
        [Display(Name = "Projectile", ResourceType = typeof(Resources))]
        [EnumMember]
        Projectile,
        [Display(Name = "AntiShipWeapon", ResourceType = typeof(Resources))]
        [EnumMember]
        AntiShipWeapon,
        [Display(Name = "AntiPlanetWeapon", ResourceType = typeof(Resources))]
        [EnumMember]
        AntiPlanetWeapon,
        #endregion
        #region buildings
        [Display(Name = "DefenceBuildings", ResourceType = typeof(Resources))]
        [EnumMember]
        DefenceBuildings,
        [Display(Name = "CivilBuilding", ResourceType = typeof(Resources))]
        [EnumMember]
        CivilBuilding,
        [Display(Name = "MilitaryBuildings", ResourceType = typeof(Resources))]
        [EnumMember]
        MilitaryBuildings,
        #endregion
        #region Enviroment
        [Display(Name = "ReducePollution", ResourceType = typeof(Resources))]
        [EnumMember]
        ReducePollution,
        [Display(Name = "EnancheProduction", ResourceType = typeof(Resources))]
        [EnumMember]
        EnancheProduction,
        [Display(Name = "BetterLiving", ResourceType = typeof(Resources))]
        [EnumMember]
        BetterLiving,
        #endregion
        #region Phisycs
        [Display(Name = "Energy", ResourceType = typeof(Resources))]
        [EnumMember]
        Energy,
        [Display(Name = "Space", ResourceType = typeof(Resources))]
        [EnumMember]
        Space,
        [Display(Name = "Materials", ResourceType = typeof(Resources))]
        [EnumMember]
        Materials,
        #endregion
        #region Social
        [Display(Name = "SocialLiving", ResourceType = typeof(Resources))]
        [EnumMember]
        SocialLiving,
        [Display(Name = "RulingSystem", ResourceType = typeof(Resources))]
        [EnumMember]
        RulingSystem,
        #endregion
        None
    }
}