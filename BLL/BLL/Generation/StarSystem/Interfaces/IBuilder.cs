using System;
using BLL.Utilities.Structs;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Stars;
using SharedDto.UtilityDto;

namespace BLL.Generation.StarSystem.Interfaces
{
    internal interface IBuilder
    {
        PlanetDto Build(StarDto star, SystemGenerationDto conditions, Random rnd, OrbitGenerator generator, DoubleRange closeRange);
    }
}
