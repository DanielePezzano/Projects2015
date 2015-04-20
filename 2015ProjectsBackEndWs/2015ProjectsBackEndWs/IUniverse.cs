using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace _2015ProjectsBackEndWs
{
    [ServiceContract]
    public interface IUniverse
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "json/{minX}/{minY}/{maxX}/{maxY}")]
        string GetUniversePortion(int minX, int minY, int maxX, int maxY);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "json/{minX}/{minY}/{maxX}/{maxY}")]
        void GeneratePortion(int minX, int minY, int maxX, int maxY);
    }
}
