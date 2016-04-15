namespace BLL.Engine.Planet.Production.Interfaces
{
    public interface IUpdater
    {
        bool UpdateToDo { get; set; }
        void CheckTimeDifference();
        void Update();

    }
}