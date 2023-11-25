using System.Security.Cryptography;

namespace ProNotes.AppLib.Tools
{
    public static class TwoFactorAuth
    {
        /// <summary>
        /// Generates a Base32 encoded, 160-bit (size of SHA1 hash) random security key
        /// </summary>
        /// <returns></returns>
        public static string GetAuthenticatorKey()
        {
            byte[] bytes = new byte[20];
            RandomNumberGenerator.Create().GetBytes(bytes);
            return Base32.ToBase32(bytes);
        }

        /// <summary>
        /// Generates a time-based one-time password (TOTP). 
        /// The TOTP is a 6 digit code which is a hash of the key and the current time.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetAuthenticatorCode(string key)
        {
            long unixTimestamp = (DateTime.UtcNow.Ticks - 621355968000000000L) / 10000000L;
            long window = unixTimestamp / 30;
            byte[] keyBytes = Base32.FromBase32(key);
            byte[] counter = BitConverter.GetBytes(window);
            if (BitConverter.IsLittleEndian) Array.Reverse(counter);

            HMACSHA1 hmac = new HMACSHA1(keyBytes);
            byte[] hash = hmac.ComputeHash(counter);
            int offset = hash[^1] & 0xf;

            // Convert the 4 bytes into an integer, ignoring the sign.
            int binary =
                (hash[offset] & 0x7f) << 24
                | hash[offset + 1] << 16
                | hash[offset + 2] << 8
                | hash[offset + 3];

            return binary % (int)Math.Pow(10, 6);
        }

        public static long GetCurrentCounter()
        {
            DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return 30 - (long)(DateTime.UtcNow - unixEpoch).TotalSeconds % 30;
        }
    }
}

// https://kenhaggerty.com/articles/article/aspnet-core-31-2fa-authenticating