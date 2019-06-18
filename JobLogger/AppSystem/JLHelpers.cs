using System;

namespace JobLogger.AppSystem
{
    public class JLHelpers
    {
        public static TimeSpan TimeFromDateTime(DateTime date) => date.TimeOfDay;
    }
}
