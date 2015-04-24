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
                if (!string.IsNullOrEmpty(time))
                {
                    DateTime serverDate = Newtonsoft.Json.JsonConvert.DeserializeObject<DateTime>(time);

                    //creo il biglietto di autenticazione. Nota che avrò un minuto di tempo per spedire il messaggio
                    string username = ConfigurationManager.AppSettings["Username"];
                    string password = ConfigurationManager.AppSettings["Password"];
                    string saltKey = ConfigurationManager.AppSettings["SaltKey"];
                    string inputKey = ConfigurationManager.AppSettings["InputKey"];
                    string callSign = "UniverseRangeDto";
                    string clientName = "WcfTester";

                    // Crea il DTO
                    UniverseRangeDto data = new UniverseRangeDto();
                    data.MinX = 30;
                    data.MaxX = 200;
                    data.MinY = 0;
                    data.MaxY = 100;
                    data.Auth = new BaseAuthDto();
                    data.Auth.GeneratedStamp = DateTime.Now;
                    data.Auth.AuthHash_01 = Sha1Managed.GetSHA1HashData(username + "_" + password + "_" + callSign + "_" + data.Auth.GeneratedStamp.ToUniversalTime());
                    data.Auth.AuthHash_02 = RijndaelManagedEncryption.EncryptRijndael(clientName + "_" + DateTime.Now.ToUniversalTime() + "_" + callSign, saltKey, inputKey);

                    //Serializzalo  Json
                    MemoryStream stream = new MemoryStream();
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(UniverseRangeDto));
                    serializer.WriteObject(stream, data);
                    stream.Position = 0;
                    string serializedData = string.Empty;
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        serializedData = reader.ReadToEnd();
                    }
                    //Criptalo in rijndael
                    serializedData = RijndaelManagedEncryption.EncryptRijndael(serializedData, saltKey, inputKey);
                    //usalo
                    result = client.RetrieveUniversePortion(serializedData);
                }
            }
            Console.Write(result);
            Console.Read();
        }
    }
}
