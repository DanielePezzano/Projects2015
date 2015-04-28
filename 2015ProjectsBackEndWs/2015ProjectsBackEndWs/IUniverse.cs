using SharedDto;
using System.ServiceModel;

namespace _2015ProjectsBackEndWs
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUniverse" in both code and config file together.
    [ServiceContract]
    public interface IUniverse
    {
        [OperationContract]
        string RetrieveUniversePortion(string data);

        [OperationContract]
        string RetrievePlanetInfo(string data);

        [OperationContract]
        string RetrieveUniverseList();
        
        [OperationContract]
        string GenerateStarSystem(PlanetGenerationDto generationData, string hashcall);

        [OperationContract]
        bool CheckUserRegistration(string data);

        [OperationContract]
        string ServiceTime();
    }
}
