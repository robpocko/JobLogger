using System.Collections.Generic;

namespace JobLogger.BF.ListModels
{
    public class TimeLineListModel
    {
        public long ID { get; internal set; }
        public string Title { get; internal set; }
        public bool IsActive { get; set; }
    }

    public class TimeLineList
    {
        public int RecordCount { get; set; }
        public List<TimeLineListModel> Data { get; set; }
    }

}
