using System.Security.Cryptography;
using System.Text;

namespace Abstra.Core.Helpers
{
    internal static class Encrypt
    {
        public static string ComputeSha512Hash(string rawData)
        {
            byte[] bytes = SHA512.HashData(Encoding.UTF8.GetBytes(rawData));
            StringBuilder builder = new();
            foreach (byte b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}
