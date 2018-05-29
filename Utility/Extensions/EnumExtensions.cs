using System;

namespace Utility.Extensions
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum value) => Enum.GetName(typeof(Enum), value);
    }
}
