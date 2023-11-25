namespace ProNotes.AppLib.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime FromUnixTime(this long unixTime)
        {
            // return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return DateTime.UnixEpoch.AddSeconds(unixTime);
        }

        public static long ToUnixTime(this DateTime dt)
        {
            int unixTimestamp = (int)dt.Subtract(DateTime.UnixEpoch).TotalSeconds;
            return unixTimestamp;
        }
    }
}