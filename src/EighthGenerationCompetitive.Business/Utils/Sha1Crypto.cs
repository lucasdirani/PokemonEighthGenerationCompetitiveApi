using System.Security.Cryptography;

namespace EighthGenerationCompetitive.Business.Utils
{
    public static class Sha1Crypto
    {
        public static byte[] ComputeHash(byte[] buffer)
        {
            using var sha1 = new SHA1CryptoServiceProvider();

            return sha1.ComputeHash(buffer);
        }
    }
}