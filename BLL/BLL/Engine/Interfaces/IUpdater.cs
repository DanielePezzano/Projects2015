namespace BLL.Engine.Interfaces
{
    public interface IUpdater
    {
        bool UpdateToDo { get; set; }

        void Update();
        void CheckTimeDifference();
    }
}