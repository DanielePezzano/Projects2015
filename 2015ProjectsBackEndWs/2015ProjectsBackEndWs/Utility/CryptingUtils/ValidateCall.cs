using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace _2015ProjectsBackEndWs.Utility
{
    public static class ValidateCall
    {
        public static bool Validate(DateTime hashDate, string authHash, string dtoSignature)
        {
            bool result = false;
            string CompareHash = ConfigurationManager.AppSettings["Username"] + "_" + ConfigurationManager.AppSettings["Password"] + "_" + dtoSignature + "_" + hashDate.ToUniversalTime();
            if ((hashDate - DateTime.Now).Minutes <= 1 &&
                Sha1Managed.ValidateSHA1HashData(CompareHash, authHash)
                )
            {
                result = true;
            }
            return result;
        }
    }
}