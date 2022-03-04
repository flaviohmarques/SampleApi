using System;
using System.Text;

namespace SampleApi.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string LettersDraw(char intervalIni, char intervalEnd, Int16 digits)
        {
            StringBuilder saida = new();
            RandomGenerator r = new();
            for (int i = 0; i < digits; i++)
            {
                int rInt = r.Next((int)intervalIni, (int)intervalEnd);
                saida.Append((char)rInt);
            }
            return saida.ToString();
        }

        public static string GetSimpleHash(this string source)
        {
            return $"{String.Format("{0:X}", source.ToString().GetHashCode())}{DateTime.Now:ddMMyy}";
        }

        public static string StringToBase64(string token)
        {
            byte[] data = Encoding.ASCII.GetBytes(token);
            return Convert.ToBase64String(data);
        }
    }
}
