using System.Collections.Generic;
using System.Threading.Tasks;
using JobLogger.AppSystem.DataAccess;

namespace JobLogger.AppSystem.UI
{
    public class FeatureIncrementalLoad : IncrementalLoadingBase
    {
        private int                 count;
        private string              title;
        private RequirementStatus?  status;

        public FeatureIncrementalLoad(string title, RequirementStatus? status)
        {
            this.title = title;
            this.status = status;
        }

        // Note that we ignore parameter 'count' because we only want to load 1 page at a time
        protected async override Task<IList<object>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, uint count)
        {
            int page = this.Count / APICommon.FETCH_SIZE;

            FeaturesListAPI data = await Features.Get(page, APICommon.FETCH_SIZE, title, status);

            this.count = data.recordCount;

            return data.data.ToArray();
        }

        protected override bool HasMoreItemsOverride()
        {
            return count > this.Count;
        }
    }
}
