using System;

namespace JobLogger.AppSystem
{
    public class JLHelpers
    {
        public static TimeSpan TimeFromDateTime(DateTime date) => date.TimeOfDay;
        public static string ShowCommentToolTip(int nbr, string entity) => string.Format("There are {0} comments for this {1}", nbr, entity);
    }
}
