using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Util.Extensions
{
    public static class ByteExtensions
    {
        public static byte[] FromBase64(this string base64) => Convert.FromBase64String(base64);
    }
}
