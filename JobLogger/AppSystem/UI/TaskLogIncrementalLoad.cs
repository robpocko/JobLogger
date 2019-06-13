using System.Collections.Generic;
using System.Threading.Tasks;
using JobLogger.AppSystem.DataAccess;

namespace JobLogger.AppSystem.UI
{
    public class TaskLogIncrementalLoad : IncrementalLoadingBase
    {
        private int count;

        // Note that we ignore parameter 'count' because we only want to load 1 page at a time
        protected async override Task<IList<object>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, uint count)
        {
            int page = this.Count / APICommon.FETCH_SIZE;

            TaskLogsListAPI data = await TaskLogs.Get(page, APICommon.FETCH_SIZE);

            this.count = data.recordCount;

            return data.data.ToArray();
        }

        protected override bool HasMoreItemsOverride()
        {
            return count > this.Count;
        }
    }
}
