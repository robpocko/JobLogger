using JobLogger.DAL.Common;
using System.Collections.Generic;

namespace JobLogger.BF.ListModels
{
    public class TaskListModel
    {
        public long ID { get; internal set; }
        public string Title { get; internal set; }
        public TaskType TaskType { get; set; }
    }

    public class TaskList
    {
        public int RecordCount { get; set; }
        public List<TaskListModel> Data { get; set; }
    }
}
