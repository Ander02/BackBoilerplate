using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Util.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64(this byte[] buffer) => Convert.ToBase64String(buffer);

        public static string RemoveAccentuation(this string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];

                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark) stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
    }
}
