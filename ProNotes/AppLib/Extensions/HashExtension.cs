using System.Security.Cryptography;
using System.Text;

namespace ProNotes.AppLib.Extensions
{
    public static class HashExtension
    {
        public static string ToMd5(this string Str)
        {
            using var sha = MD5.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(Str)));
        }

        public static string ToSha1(this string Str)
        {
            using var sha = SHA1.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(Str)));
        }

        public static string ToSha256(this string Str)
        {
            using var sha = SHA256.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(Str)));
        }

        public static string ToSha384(this string Str)
        {
            using var sha = SHA384.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(Str)));
        }

        public static string ToSha512(this string Str)
        {
            using var sha = SHA512.Create();
            return Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(Str)));
        }
    }
}