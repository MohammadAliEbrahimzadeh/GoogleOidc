using System.Security.Cryptography;
using System.Text;

namespace GoogleOidcTest.Helpers
{
        public static class HashGenerator
        {
                public static string GenerateHash(this string plainText)
                {
                        byte[] bytesArray = Encoding.Unicode.GetBytes(plainText);
                        byte[] hashed = HashAlgorithm.Create("MD5").ComputeHash(bytesArray);
                        return Convert.ToBase64String(hashed);
                }
        }
}
