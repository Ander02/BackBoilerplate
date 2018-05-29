using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsDefaultDateTime(this DateTime date) => date.Equals(default(DateTime));
    }
}
