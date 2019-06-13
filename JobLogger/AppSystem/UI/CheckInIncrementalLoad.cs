using System.Collections.Generic;
using System.Threading.Tasks;
using JobLogger.AppSystem.DataAccess;

namespace JobLogger.AppSystem.UI
{
    public class CheckInIncrementalLoad : IncrementalLoadingBase
    {
        private int count;
        private string comment;
        private long? codeBranchID;

        public CheckInIncrementalLoad(string comment, long? codeBranchID)
        {
            this.comment = comment;
            this.codeBranchID = codeBranchID;
        }

        // Note that we ignore parameter 'count' because we only want to load 1 page at a time
        protected async override Task<IList<object>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, uint count)
        {
            int page = this.Count / APICommon.FETCH_SIZE;

            CheckInsListAPI data = await CheckIns.Get(page, APICommon.FETCH_SIZE, comment, codeBranchID);

            this.count = data.recordCount;

            return data.data.ToArray();
        }

        protected override bool HasMoreItemsOverride()
        {
            return count > this.Count;
        }
    }
}
