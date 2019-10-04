using System;
using System.Collections.Generic;

namespace JobLogger.BF.ListModels
{
    public class TaskLogListModel
    {
        public long ID { get; internal set; }
        public string Description { get; internal set; }
        public DateTime LogDate { get; internal set; }
        public TimeSpan StartTime { get; internal set; }
    }

    public class TaskLogList
    {
        public int RecordCount { get; set; }
        public List<TaskLogListModel> Data { get; set; }
    }
}
