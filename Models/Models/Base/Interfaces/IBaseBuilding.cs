
namespace Models.Base.Interfaces
{
    public interface IBaseBuilding
    {
        string Name { get; set; }
        string Description { get; set; }

        int SpaceNeeded { get; set; }
        int UsedSpaces { get; set; }
        int Number { get; set; }
    }
}
