using JobLogger.DAL.Common;
using System.Collections.Generic;

namespace JobLogger.BF.ListModels
{
    public class RequirementListModel
    {
        public long ID { get; internal set; }
        public string Title { get; internal set; }
        public RequirementStatus Status { get; set; }
    }

    public class RequirementList
    {
        public int RecordCount { get; set; }
        public List<RequirementListModel> Data { get; set; }
    }
}
