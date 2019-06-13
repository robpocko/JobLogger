using System.Collections.Generic;

namespace JobLogger.BF.ListModels
{
    public class CodeBranchListModel
    {
        public long ID { get; internal set; }
        public string Name { get; internal set; }
    }

    public class CodeBranchList
    {
        public int RecordCount { get; set; }
        public List<CodeBranchListModel> Data { get; set; }
    }
}
