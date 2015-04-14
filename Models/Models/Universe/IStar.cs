using Models.Universe.Enum;
using Models.Universe.Strcut;
using System;
using System.Collections.Generic;
namespace Models.Universe
{
    public interface IStar
    {
        Coordinates Coordinate { get; set; }
        double Mass { get; set; }
        string Name { get; set; }
        int RadiationLevel { get; set; }
        double Radius { get; set; }
        List<Satellite> Satellites { get; set; }
        StarColor StarColor { get; set; }
        StarType StarType { get; set; }
        int SurfaceTemp { get; set; }
        Galaxy Universe { get; set; }
    }
}
