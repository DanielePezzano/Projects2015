using System;
using BLL.Utilities.Structs;
using Models.Base;
using Models.Universe;

namespace BLL.Generation.StarSystem.Builders
{
    internal interface IBuilder
    {
        BaseEntity Build(Star star, PlanetCustomConditions conditions, Random rnd, OrbitGenerator generator, DoubleRange closeRange);
    }
}
