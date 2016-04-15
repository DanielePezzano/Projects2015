using System;
using SharedDto.UtilityDto;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.Sector.IstanceFactory
{
    public static class FactoryGenerator
    {
        public static GenerateSector RetrieveGenerateSector(int alreadyPresentStars, IUnitOfWork uow, Random rnd,SystemGenerationDto systemGenerationDto)
        {
            return new GenerateSector(alreadyPresentStars,uow,rnd,systemGenerationDto);
        }
    }
}