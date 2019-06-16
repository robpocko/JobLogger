using JobLogger.BF.ReportModels;
using JobLogger.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobLogger.BF.ReportBF
{
    public class TimesheetReportBF
    {
        private JobLoggerDbContext db;

        public TimesheetReportBF(JobLoggerDbContext db)
        {
            this.db = db;
        }

        public List<TimesheetReportModel> GetTimesheetForDay(DateTime day)
        {
            try
            {
                var items = db.TaskLogs
                                .Where(l => l.LogDate == day.Date)
                                .OrderBy(l => l.StartTime)
                                .ToList();

                return (from taskLog in db.TaskLogs
                        where taskLog.LogDate == day.Date
                        select new TimesheetReportModel
                        {
                            TaskStartTime = taskLog.StartTime,
                            TaskEndTime = taskLog.EndTime.Value,
                            Description = taskLog.Description,
                            TaskDuration = (int)((taskLog.EndTime.Value - taskLog.StartTime).TotalMinutes)
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
