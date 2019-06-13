using System.Collections.Generic;

namespace JobLogger.BF.ListModels
{
    public class CheckInListModel
    {
        public long ID { get; internal set; }
        public string Comment { get; internal set; }
    }

    public class CheckInList
    {
        public int RecordCount { get; set; }
        public List<CheckInListModel> Data { get; set; }
    }
}
