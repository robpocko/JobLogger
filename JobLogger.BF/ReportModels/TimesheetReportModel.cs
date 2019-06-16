using System;

namespace JobLogger.BF.ReportModels
{
    public class TimesheetReportModel
    {
        public TimeSpan TaskStartTime { get; set; }
        public TimeSpan TaskEndTime { get; set; }
        public int TaskDuration { get; set; }
        public string Description { get; set; }
    }
}
