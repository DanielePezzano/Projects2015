using System;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.Sector.Factories
{
    public static class FactoryGenerator
    {
        public static GenerateSector RetrieveGenerateSector(int alreadyPresentStars, int minX, int maxX, int minY, int maxY, IUnitOfWork uow, Random rnd, int galaxyId)
        {
            return new GenerateSector(alreadyPresentStars, minX, maxX, minY, maxY, uow, rnd, galaxyId);
        }
    }
}