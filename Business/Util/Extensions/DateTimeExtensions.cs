using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Util.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsDefaultDateTime(this DateTime date) => date.Equals(default(DateTime));
    }
}
