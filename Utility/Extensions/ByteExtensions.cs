﻿using System;

namespace Utility.Extensions
{
    public static class ByteExtensions
    {
        public static byte[] FromBase64(this string base64) => Convert.FromBase64String(base64);
    }
}
