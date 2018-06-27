using System;

namespace Utility.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsDefaultDateTime(this DateTime date) => date.Equals(default(DateTime));
    }
}
