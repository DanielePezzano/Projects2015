
using System.ComponentModel.DataAnnotations;
namespace Models.Tech.Enum
{
    public enum TechnologyField
    {
        [Display(Name = "Buildings",ResourceType = typeof(Resources))]
        Buildings,      //
        [Display(Name = "Social", ResourceType = typeof(Resources))]
        Social,         //
        [Display(Name = "Phisycs", ResourceType = typeof(Resources))]
        Phisycs,        //
        [Display(Name = "ShipComponent", ResourceType = typeof(Resources))]
        ShipComponent,  //
        [Display(Name = "ShipFrame", ResourceType = typeof(Resources))]
        ShipFrame,      //
        [Display(Name = "Weapons", ResourceType = typeof(Resources))]
        Weapons,        //
        [Display(Name = "Enviroment", ResourceType = typeof(Resources))]
        Enviroment,     //
        [Display(Name = "Mathematics", ResourceType = typeof(Resources))]
        Mathematics,
    }

    public enum TechnologySubField
    {
        #region Shipcomponent
        [Display(Name = "Engine", ResourceType = typeof(Resources))]
        Engine,
        [Display(Name = "Armor", ResourceType = typeof(Resources))]
        Armor,
        [Display(Name = "Shield", ResourceType = typeof(Resources))]
        Shield,
        [Display(Name = "Tools", ResourceType = typeof(Resources))]
        Tools, 
        #endregion
        #region ShipFrame
        [Display(Name = "Frame", ResourceType = typeof(Resources))]
        Frame,
        #endregion
        #region Weapons
        [Display(Name = "Bombs", ResourceType = typeof(Resources))]
        Bombs,
        [Display(Name = "Beams", ResourceType = typeof(Resources))]
        Beams,
        [Display(Name = "Projectile", ResourceType = typeof(Resources))]
        Projectile,
         [Display(Name = "AntiShipWeapon", ResourceType = typeof(Resources))]
        AntiShipWeapon,
         [Display(Name = "AntiPlanetWeapon", ResourceType = typeof(Resources))]
        AntiPlanetWeapon,
        #endregion
        #region buildings
        [Display(Name = "DefenceBuildings", ResourceType = typeof(Resources))]
        DefenceBuildings,
        [Display(Name = "CivilBuilding", ResourceType = typeof(Resources))]
        CivilBuilding,
        [Display(Name = "MilitaryBuildings", ResourceType = typeof(Resources))]
        MilitaryBuildings,
        #endregion
        #region Enviroment
        [Display(Name = "ReducePollution", ResourceType = typeof(Resources))]
        ReducePollution,
        [Display(Name = "EnancheProduction", ResourceType = typeof(Resources))]
        EnancheProduction,
        [Display(Name = "BetterLiving", ResourceType = typeof(Resources))]
        BetterLiving,
        #endregion
        #region Phisycs
        [Display(Name = "Energy", ResourceType = typeof(Resources))]
        Energy,
        [Display(Name = "Space", ResourceType = typeof(Resources))]
        Space,
        [Display(Name = "Materials", ResourceType = typeof(Resources))]
        Materials,
        #endregion
        #region Social
        [Display(Name = "SocialLiving", ResourceType = typeof(Resources))]
        SocialLiving,
        [Display(Name = "RulingSystem", ResourceType = typeof(Resources))]
        RulingSystem,
        #endregion
        None
    }
}
