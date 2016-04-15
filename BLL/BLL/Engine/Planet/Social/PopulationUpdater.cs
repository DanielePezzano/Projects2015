using BLL.Engine.Interfaces;
using BLL.Engine.Planet.Social.Interfaces;

namespace BLL.Engine.Planet.Social
{
    public class PopulationUpdater :ISocialUpdater, IUpdater
    {

        public bool UpdateToDo { get; set; }

        #region private methods
        #endregion

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void CheckTimeDifference()
        {
            throw new System.NotImplementedException();
        }
    }
}