using System;
using System.Linq;

namespace FW.Common.Utilities
{
    public static class TokenUtils
    {
        public static string GenerateEmailConfirmationToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            token = token.Replace("=", "");
            token = token.Replace("+", "");
            token = token.Replace("/", "");
            return token;
        }
    }
}
