using System;
using System.IO;

namespace FW.Common.Helpers
{
    public static class CommonMethods
    {
        public static string GenarateItemCodeByDateTime()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static byte[] ConvertStreamToByte(Stream stream)
        {
            var target = new MemoryStream();
            stream.CopyTo(target);
            return target.ToArray();
        }

        public static long? ToNullableLong(this string s)
        {
            if (long.TryParse(s, out long i)) return i;

            return null;
        }
    }
}
