
using System;
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
        public static double RandomDouble(int min, int max, Random rnd)
        {
            return rnd.NextDouble() * (max - min) + min;
        }
    }
}
