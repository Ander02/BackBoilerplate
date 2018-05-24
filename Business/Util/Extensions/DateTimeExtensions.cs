using System;

namespace Business.Util.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsDefaultDateTime(this DateTime date) => date.Equals(default(DateTime));
    }
}
