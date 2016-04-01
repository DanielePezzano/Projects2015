using System;
using BLL.Generation.StarSystem.Factories;
using BLL.Utilities.Structs;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.Sector
{
    public class GenerateSector
    {
        private readonly int _alreadyPresentStars;
        private readonly IntRange _rangeX;
        private readonly IntRange _rangeY;
        private IUnitOfWork _uow;

        public GenerateSector(int alreadyPresentStars, int minX, int maxX, int minY, int maxY, IUnitOfWork uow, Random rnd)
        {
            _alreadyPresentStars = alreadyPresentStars;
            _rangeX = FactoryGenerator.RetrieveIntRange(minX, maxX);
            _rangeY = FactoryGenerator.RetrieveIntRange(minY, maxY);
            _uow = uow;
        }

        
    }
}
