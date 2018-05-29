﻿using System;
using System.Globalization;
using System.Text;

namespace Utility.Extensions
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
                char character = normalizedString[i];

                if (CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark) stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }
    }
}