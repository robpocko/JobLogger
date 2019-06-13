using System.Collections.Generic;
using System.Threading.Tasks;
using JobLogger.AppSystem.DataAccess;

namespace JobLogger.AppSystem.UI
{
    public class RequirementIncrementalLoad : IncrementalLoadingBase
    {
        private int                 count;
        private string              title;
        private RequirementStatus?  status;

        public RequirementIncrementalLoad(string title, RequirementStatus? status)
        {
            this.title = title;
            this.status = status;
        }

        // Note that we ignore parameter 'count' because we only want to load 1 page at a time
        protected async override Task<IList<object>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, uint count)
        {
            int page = this.Count / APICommon.FETCH_SIZE;

            RequirementsListAPI data = await Requirements.Get(page, APICommon.FETCH_SIZE, title, status);

            this.count = data.recordCount;

            return data.data.ToArray();
        }

        protected override bool HasMoreItemsOverride()
        {
            return count > this.Count;
        }
    }
}
