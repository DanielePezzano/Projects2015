using System;
using DAL.Operations.IstanceFactory;
using SharedDto.UtilityDto;

namespace BLL.Generation.Sector.IstanceFactory
{
    public static class FactoryGenerator
    {
        public static GenerateSector RetrieveGenerateSector(int alreadyPresentStars, Random rnd, SystemGenerationDto systemGenerationDto, OpFactory opFactory)
        {
            return new GenerateSector(alreadyPresentStars,rnd,systemGenerationDto,opFactory);
        }
    }
}