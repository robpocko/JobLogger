using JobLogger.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobLogger.API.Model
{
    public class CheckInAPI : APITFS
    {
        public string               Comment { get; set; }
        public DateTime             CheckInTime { get; set; }
        public long?                TaskLogID { get; set; }
        public TaskLogAPI           TaskLog { get; set; }
        public long                 CodeBranchID {get; set; }
        public CodeBranchAPI        CodeBranch { get; set; }
        public List<TaskCheckInAPI> TaskCheckIns { get; set; }

        public static CheckIn To(CheckInAPI item)
        {
            return new CheckIn
            {
                ID = item.ID,
                Comment = item.Comment,
                CheckInTime = item.CheckInTime,
                TaskLogID = item.TaskLogID,
                TaskLog = item.TaskLog != null ? TaskLogAPI.To(item.TaskLog) : null,
                CodeBranchID = item.CodeBranchID,
                CodeBranch = item.CodeBranch != null ? CodeBranchAPI.To(item.CodeBranch) : null,
                TaskCheckIns = TaskCheckInAPI.To(item.TaskCheckIns),
                IsNew = item.IsNew
            };
        }

        public static CheckInAPI From(CheckIn item)
        {
            return new CheckInAPI
            {
                ID = item.ID,
                Comment = item.Comment,
                CheckInTime = item.CheckInTime,
                TaskLogID = item.TaskLogID,
                CodeBranchID = item.CodeBranchID,
                CodeBranch = item.CodeBranch != null ? CodeBranchAPI.From(item.CodeBranch) : null,
                TaskCheckIns = TaskCheckInAPI.From(item.TaskCheckIns).ToList(),
                IsNew = item.IsNew
            };
        }

        public static ICollection<CheckIn> To(ICollection<CheckInAPI> items)
        {
            if (items != null)
            {
                ICollection<CheckIn> list = new List<CheckIn>();
                foreach (var item in items)
                {
                    list.Add(To(item));
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        public static ICollection<CheckInAPI> From(ICollection<CheckIn> items)
        {
            if (items != null)
            {
                ICollection<CheckInAPI> list = new List<CheckInAPI>();
                foreach (var item in items)
                {
                    list.Add(From(item));
                }
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
