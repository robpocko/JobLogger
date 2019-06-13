﻿using JobLogger.DAL;
using System;
using System.Collections.Generic;

namespace JobLogger.API.Model
{
    public class CheckInAPI : APIBase
    {
        public string Comment { get; set; }
        public DateTime CheckInTime { get; set; }
        public long? TaskLogID { get; set; }
        public TaskLogAPI TaskLog { get; set; }
        public long CodeBranchID {get; set; }
        public CodeBranchAPI CodeBranch { get; set; }
        ICollection<TaskCheckInAPI> TaskCheckIns { get; set; }

        public static CheckIn To(CheckInAPI item)
        {
            return new CheckIn
            {
                ID = item.ID,
                Comment = item.Comment,
                CheckInTime = item.CheckInTime,
                TaskLogID = item.TaskLogID,
                TaskLog = TaskLogAPI.To(item.TaskLog),
                CodeBranchID = item.CodeBranchID,
                CodeBranch = item.CodeBranch != null ? CodeBranchAPI.To(item.CodeBranch) : null,
                TaskCheckIns = TaskCheckInAPI.To(item.TaskCheckIns)
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
                TaskLog = item.TaskLog != null ? TaskLogAPI.From(item.TaskLog) : null,
                CodeBranchID = item.CodeBranchID,
                CodeBranch = item.CodeBranch != null ? CodeBranchAPI.From(item.CodeBranch) : null,
                TaskCheckIns = TaskCheckInAPI.From(item.TaskCheckIns)
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
