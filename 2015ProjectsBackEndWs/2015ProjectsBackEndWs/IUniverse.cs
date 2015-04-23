using _2015ProjectsBackEndWs.DTO.Universe;
using _2015ProjectsBackEndWs.DTO.UtilityDto;
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
        List<StarDto> GetUniversePortion(UniverseRangeDto universeRage);

        [OperationContract]
        string GenerateStarSystem(PlanetGenerationDto generationData, string hashcall);
    }
}
