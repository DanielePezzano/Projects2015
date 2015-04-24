using _2015ProjectsBackEndWs.DTO.Universe;
using _2015ProjectsBackEndWs.DTO.UtilityDto;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace _2015ProjectsBackEndWs
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUniverse" in both code and config file together.
    [ServiceContract]
    public interface IUniverse
    {
        [OperationContract]
        string RetrieveUniversePortion(string data);

        [OperationContract]
        string RetrieveUniversePortionUnused(UniverseRangeDto data); // definito per permettere di esporre ai client, il data-transport-object UniverseRangeDto 

        [OperationContract]
        string GenerateStarSystem(PlanetGenerationDto generationData, string hashcall);

        [OperationContract]
        string ServiceTime();
    }
}
