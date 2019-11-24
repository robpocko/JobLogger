using System.Collections.Generic;
using System.Threading.Tasks;
using JobLogger.AppSystem.DataAccess;

namespace JobLogger.AppSystem.UI
{
    public class CodeBranchIncrementalLoad : IncrementalLoadingBase
    {
        private int count;
        private string codeBranch;
        private bool showInactive;

        public CodeBranchIncrementalLoad(string codeBranch, bool showInactive)
        {
            this.codeBranch = codeBranch;
            this.showInactive = showInactive;
        }

        // Note that we ignore parameter 'count' because we only want to load 1 page at a time
        protected async override Task<IList<object>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, uint count)
        {
            int page = this.Count / APICommon.FETCH_SIZE;

            CodeBranchesListAPI data = await CodeBranches.Get(page, APICommon.FETCH_SIZE);

            this.count = data.recordCount;

            return data.data.ToArray();
        }

        protected override bool HasMoreItemsOverride()
        {
            return count > this.Count;
        }
    }
}
