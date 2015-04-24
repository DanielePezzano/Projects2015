using _2015ProjectsBackEndWs.DTO;
using _2015ProjectsBackEndWs.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WcfCommCrypto;

namespace _2015ProjectsBackEndWs.Security
{
    public static class Validation
    {
        public static bool Validate(BaseAuthDto dto,string callInstanceName)
        {
            return ValidateCall.Validate(
                        dto.GeneratedStamp,
                        dto.AuthHash_01,
                        dto.AuthHash_02,
                        ConfigurationManager.AppSettings[ConfAppSettings.SaltKey],
                        ConfigurationManager.AppSettings[ConfAppSettings.InputKey],
                        callInstanceName,
                        ConfigurationManager.AppSettings[ConfAppSettings.Username],
                        ConfigurationManager.AppSettings[ConfAppSettings.Password],
                        AcceptedClients.WhiteList,
                        CallSeparators.defaultSeparator);
        }
    }
}