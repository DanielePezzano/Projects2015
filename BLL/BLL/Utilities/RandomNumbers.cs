
using System;
using System.Linq;

namespace BLL.Utilities
{
    public static class RandomNumbers
    {
        /// <summary>
        /// Generate a random integer
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static int RandomInt(int min, int max , Random rnd)
        {
            return rnd.Next(min, max);
        }
        /// <summary>
        /// Generate a random double
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        public static double RandomDouble(double min, double max, Random rnd)
        {
            return Math.Truncate((rnd.NextDouble() * (max - min) + min)*100)/100;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
