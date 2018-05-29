using System;
using System.Text;

namespace Utility.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64(this byte[] buffer) => Convert.ToBase64String(buffer);

        public static string NormalizeString(this string text) => Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(text)).ToUpper();
    }
}
