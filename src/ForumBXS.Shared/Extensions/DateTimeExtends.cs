namespace System
{
    public static class DateTimeExtends
    {
        public static DateTime BR(this DateTime value)
        {
            var cstZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(value, cstZone);
        }
    }
}