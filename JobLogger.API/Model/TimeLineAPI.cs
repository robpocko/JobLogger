using JobLogger.DAL;
using JobLogger.DAL.Common;
using System.Collections.Generic;
using System.Linq;

namespace JobLogger.API.Model
{
    public class TimeLineAPI : APIBase
    {
        public string Title { get; set; }

        public bool IsActive { get; set; }

        public static TimeLine To(TimeLineAPI item)
        {
            return new TimeLine
            {
                ID = item.ID,
                Title = item.Title,
                IsActive = item.IsActive
            };
        }

        public static TimeLineAPI From(TimeLine item)
        {
            return new TimeLineAPI
            {
                ID = item.ID,
                Title = item.Title,
                IsActive = item.IsActive
            };
        }
    }
}
