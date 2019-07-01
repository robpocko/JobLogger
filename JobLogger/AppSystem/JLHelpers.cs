using JobLogger.AppSystem.DataAccess;
using System;
using System.Collections.Generic;

namespace JobLogger.AppSystem
{
    public class JLHelpers
    {
        public static TimeSpan TimeFromDateTime(DateTime date) => date.TimeOfDay;
        public static string ShowCommentToolTip(int nbr, string entity) => string.Format("There are {0} comments for this {1}", nbr, entity);

        internal static bool EnableShowRequirementCommentsButton(List<RequirementCommentAPI> comments) => comments != null && comments.Count > 0;
        internal static bool EnableShowTaskCommentsButton(List<TaskCommentAPI> comments) => comments != null && comments.Count > 0;
        internal static bool EnableShowTaskLogCommentsButton(List<TaskLogCommentAPI> comments) => comments != null && comments.Count > 0;

    }
}
