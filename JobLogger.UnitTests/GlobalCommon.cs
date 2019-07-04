using JobLogger.API.Model;
using JobLogger.BF;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace JobLogger.UnitTests
{
    public class GlobalCommon
    {
        public static bool dataHasBeenCleared = false;

        public static void ClearData()
        {
            using (JobLoggerDbContext db = new JobLoggerDbContext())
            {
                db.Database.ExecuteSqlCommand("delete from CodeBranch");
                db.Database.ExecuteSqlCommand("delete from TaskLog");
                db.Database.ExecuteSqlCommand("delete from Task");
                db.Database.ExecuteSqlCommand("delete from Requirement");
                db.Database.ExecuteSqlCommand("delete from Feature");

                new CodeBranchBF(db).Create(new CodeBranch { Name = "Code Branch 1" });
            }
            dataHasBeenCleared = true;
        }

        internal static FeatureAPI NewFeature(long id, string title)
        {
            return new FeatureAPI
            {
                ID = id,
                IsNew = true,
                Title = title,
                Status = RequirementStatus.Active,
                Requirements = new List<RequirementAPI>()
            };
        }

        internal static RequirementAPI NewRequirement(long id, string title)
        {
            return new RequirementAPI
            {
                ID = id,
                IsNew = true,
                Title = title,
                Status = RequirementStatus.Active,
                Comments = new List<RequirementCommentAPI>(),
                Tasks = new List<TaskAPI>()
            };
        }

        internal static TaskAPI NewTask(long id, string title)
        {
            return new TaskAPI
            {
                ID = id,
                IsNew = true,
                IsActive = true,
                TaskType = TaskType.Task,
                Title = title,
                CheckIns = new List<TaskCheckInAPI>(),
                Comments = new List<TaskCommentAPI>(),
                Logs = new List<TaskLogAPI>()
            };
        }

        internal static TaskLogAPI NewTaskLog(DateTime logDate)
        {
            return new TaskLogAPI
            {
                Description = "Comment for Log",
                LogDate = logDate.Date,
                StartTime = logDate.TimeOfDay,
                EndTime = logDate.AddHours(2).TimeOfDay,
                CheckIns = new List<CheckInAPI>(),
                Comments = new List<TaskLogCommentAPI>()
            };
        }

        internal static CheckInAPI NewCheckIn(long id, DateTime checkInTime, long codeBranchId, long taskId)
        {
            return new CheckInAPI
            {
                ID = id,
                IsNew = true,
                CheckInTime = checkInTime,
                CodeBranchID = codeBranchId,
                Comment = $"Check In at {checkInTime.ToString("HH:mm")}",
                TaskCheckIns = new List<TaskCheckInAPI>
                {
                    new TaskCheckInAPI { TaskID = taskId }
                }
            };
        }
    }
}
