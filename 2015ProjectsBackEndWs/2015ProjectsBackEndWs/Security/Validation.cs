using System.Configuration;
using SharedDto;
using WcfCommCrypto;

namespace _2015ProjectsBackEndWs.Security
{
    public static class Validation
    {
        public static bool Validate(BaseAuthDto dto, string callInstanceName)
        {
            return ValidateCall.Validate(
                dto.GeneratedStamp,
                dto.AuthHash01,
                dto.AuthHash02,
                ConfigurationManager.AppSettings[ConfAppSettings.SaltKey],
                ConfigurationManager.AppSettings[ConfAppSettings.InputKey],
                callInstanceName,
                ConfigurationManager.AppSettings[ConfAppSettings.Username],
                ConfigurationManager.AppSettings[ConfAppSettings.Password],
                AcceptedClients.WhiteList,
                CallSeparators.DefaultSeparator);
        }
    }
}