using System;
using System.Configuration;

namespace WcfTester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //PlanetGenerationDto dto = new PlanetGenerationDto();
            var result = string.Empty;
            //var username = ConfigurationManager.AppSettings["Username"];
            //var password = ConfigurationManager.AppSettings["Password"];
            //var saltKey = ConfigurationManager.AppSettings["SaltKey"];
            //var inputKey = ConfigurationManager.AppSettings["InputKey"];
            //var callSign = "UniverseRangeDto";
            //var clientName = "WcfTester";
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


            //using (var client = new UniverseClient())
            //{
            //    ////Richiedi il tempo per la sincronizzazione, al servizio!
            //    //var time = client.ServiceTime();
            //    //var serializedData = string.Empty;
            //    //if (!string.IsNullOrEmpty(time))
            //    //{
            //    //    var serverDate = JsonConvert.DeserializeObject<DateTime>(time);
            //    //    //Esempio di chiamata per avere un settore della galassia
            //    //    //serializedData = GetSerializedDto(username, password, saltKey, inputKey, callSign, clientName, serverDate);
            //    //    ////usalo
            //    //    //result = client.RetrieveUniversePortion(serializedData);

            //    //    //Esempio di chiamata per avere informazioni su un pianeta specifico
            //    //    serializedData = GetSerializedInfoDto(username, password, saltKey, inputKey, "RetrievingInfoDto",
            //    //        clientName, 2010, serverDate);
            //    //    result = client.RetrievePlanetInfo(serializedData);
            //    //}
            //}
            Console.Write(result);
            Console.Read();
        }
    }
}