namespace System
{
    public static class DateTimeExtends
    {
        public static DateTime BR(this DateTime value)
        {
            var differenceWithUtc = -3;
            var dateBR = value.AddHours(differenceWithUtc);
            return dateBR;
        }
    }
}