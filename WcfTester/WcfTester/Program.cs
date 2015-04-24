using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfTester.UninverseWcf;
using System.Configuration;
using WcfTester.Utility;
using System.IO;
using System.Runtime.Serialization.Json;
using WcfCommCrypto;
namespace WcfTester
{
    class Program
    {
        static void Main(string[] args)
        {
            //PlanetGenerationDto dto = new PlanetGenerationDto();
            string result = string.Empty;
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];
            string saltKey = ConfigurationManager.AppSettings["SaltKey"];
            string inputKey = ConfigurationManager.AppSettings["InputKey"];
            string callSign = "UniverseRangeDto";
            string clientName = "WcfTester";
            //dto.FoodPoor = false;
            //dto.FoodRich = false;
            //dto.ForceLiving = false;
            //dto.ForceWater = false;
            //dto.MineralPoor = false;
            //dto.MineralRich = false;
            //dto.MostlyWater = false;

            //dto.MinX = 50;
            //dto.MaxX = 100;
            //dto.MinY = 50;
            //dto.MaxY = 100;

            //using (UniverseClient client = new UniverseClient())
            //{
            //    string hashCall = RijndaelManagedEncryption.EncryptRijndael(CallStartSentences.GeneratePortion + "123_ MinX = 50, MaxX = 100, MinY = 50, MaxY = 100");
            //    result = client.GenerateStarSystem(dto, hashCall);
            //}



            using (UniverseClient client = new UniverseClient())
            {
                //Richiedi il tempo per la sincronizzazione, al servizio!
                string time = client.ServiceTime();
                string serializedData = string.Empty;
                if (!string.IsNullOrEmpty(time))
                {
                    DateTime serverDate = Newtonsoft.Json.JsonConvert.DeserializeObject<DateTime>(time);
                    //Esempio di chiamata per avere un settore della galassia
                    //serializedData = GetSerializedDto(username, password, saltKey, inputKey, callSign, clientName, serverDate);
                    ////usalo
                    //result = client.RetrieveUniversePortion(serializedData);

                    //Esempio di chiamata per avere informazioni su un pianeta specifico
                    serializedData = GetSerializedInfoDto(username, password, saltKey, inputKey, "RetrievingInfoDto", clientName, 2010, serverDate);
                    result = client.RetrievePlanetInfo(serializedData);
                }
            }
            Console.Write(result);
            Console.Read();
        }
        private static string GetSerializedInfoDto(string username, string password, string saltKey, string inputKey, string callSign, string clientName, int id, DateTime serverDate)
        {
            string serializedData = string.Empty;
            RetrievingInfoDto infoDto = new RetrievingInfoDto();
            infoDto.Id = id;
            infoDto.Auth = PrepareAuthMessage(username, password, callSign, clientName, saltKey, inputKey, serverDate);
            serializedData = ApplyEncription(saltKey, inputKey, serializedData, typeof(RetrievingInfoDto), infoDto);
            return serializedData;
        }
        private static string GetSerializedDto(string username, string password, string saltKey, string inputKey, string callSign, string clientName, DateTime serverDate)
        {
            string serializedData = string.Empty;
            //creo il biglietto di autenticazione. Nota che avrò un minuto di tempo per spedire il messaggio
            // Crea il DTO
            UniverseRangeDto data = new UniverseRangeDto();
            data.MinX = 30;
            data.MaxX = 200;
            data.MinY = 0;
            data.MaxY = 100;
            data.Auth = PrepareAuthMessage(username, password, callSign, clientName, saltKey, inputKey, serverDate);

            //Serializzalo  Json
            serializedData = ApplyEncription(saltKey, inputKey, serializedData, typeof(UniverseRangeDto), data);
            return serializedData;
        }

        private static string ApplyEncription(string saltKey, string inputKey, string serializedData, Type objectType, dynamic objectClass)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectType);
            serializer.WriteObject(stream, objectClass);
            stream.Position = 0;

            using (StreamReader reader = new StreamReader(stream))
            {
                serializedData = reader.ReadToEnd();
            }
            //Criptalo in rijndael
            serializedData = RijndaelManagedEncryption.EncryptRijndael(serializedData, saltKey, inputKey);
            return serializedData;
        }

        private static BaseAuthDto PrepareAuthMessage(string username, string password, string callSign, string clientName, string saltKey, string inputKey, DateTime serverDate)
        {
            BaseAuthDto result = new BaseAuthDto();
            result.GeneratedStamp = DateTime.Now;
            result.AuthHash_01 = Sha1Managed.GetSHA1HashData(username + "_" + password + "_" + callSign + "_" + result.GeneratedStamp.ToUniversalTime());
            result.AuthHash_02 = RijndaelManagedEncryption.EncryptRijndael(clientName + "_" + serverDate.ToUniversalTime() + "_" + callSign, saltKey, inputKey);

            return result;
        }
    }
}
