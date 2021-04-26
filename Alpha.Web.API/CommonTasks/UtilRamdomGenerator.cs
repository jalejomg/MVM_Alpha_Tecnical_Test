using System;
using System.Linq;

namespace Alpha.Tests.Util
{
    /// <summary>
    /// This class contains logic to generate a string of a certain amount of characters 
    /// </summary>
    public static class UtilRandomGenerator
    {
        public static string GenerateString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
