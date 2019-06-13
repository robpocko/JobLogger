using JobLogger.DAL.Common;
using System.Collections.Generic;

namespace JobLogger.BF.ListModels
{
    public class FeatureListModel
    {
        public long ID { get; internal set; }
        public string Title { get; internal set; }
        public RequirementStatus Status { get; set; }
    }

    public class FeatureList
    {
        public int RecordCount { get; set; }
        public List<FeatureListModel> Data { get; set; }
    }
}
