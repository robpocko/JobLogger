using System.Collections.Generic;
using System.Threading.Tasks;
using JobLogger.AppSystem.DataAccess;

namespace JobLogger.AppSystem.UI
{
    public class TaskIncrementalLoad : IncrementalLoadingBase
    {
        private int count;
        private string title;
        private TaskType? taskType;
        private bool showInactive;

        public TaskIncrementalLoad(string title, TaskType? taskType, bool showInactive)
        {
            this.title = title;
            this.taskType = taskType;
            this.showInactive = showInactive;
        }

        // Note that we ignore parameter 'count' because we only want to load 1 page at a time
        protected async override Task<IList<object>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, uint count)
        {
            int page = this.Count / APICommon.FETCH_SIZE;

            TasksListAPI data = await Tasks.Get(page, APICommon.FETCH_SIZE, showInactive, title, taskType);

            this.count = data.recordCount;

            return data.data.ToArray();
        }

        protected override bool HasMoreItemsOverride()
        {
            return count > this.Count;
        } 
    }
}
