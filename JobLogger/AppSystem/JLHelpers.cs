using JobLogger.AppSystem.DataAccess;
using System;
using System.Collections.Generic;

namespace JobLogger.AppSystem
{
    public class JLHelpers
    {
        public static TimeSpan TimeFromDateTime(DateTime date) => date.TimeOfDay;
        public static string ShowCommentToolTip(int nbr, string entity) => string.Format("There are {0} comments for this {1}", nbr, entity);
        internal static bool EnableShowCommentsButton<T>(List<T> comments) where T : CommentAPI => comments != null && comments.Count > 0;
    }
}
