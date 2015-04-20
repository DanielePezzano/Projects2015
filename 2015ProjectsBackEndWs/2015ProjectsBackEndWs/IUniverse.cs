using _2015ProjectsBackEndWs.DTO.UtilityDto;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace _2015ProjectsBackEndWs
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUniverse" in both code and config file together.
    [ServiceContract]
    public interface IUniverse
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "json/{minX}/{minY}/{maxX}/{maxY}")]
        string GetUniversePortion(int minX, int minY, int maxX, int maxY);

        [OperationContract]
        void GeneratePortion(PlanetGenerationDto generationData);
    }
}
