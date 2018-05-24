using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Util.Extensions
{
    public static class EnumExtensions
    {
        public static string GetName(this Enum value) => Enum.GetName(typeof(Enum), value);
    }
}
