﻿using JobLogger.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobLogger.API.Model
{
    public class TaskLogAPI : APIBase
    {
        public DateTime                 LogDate { get; set; }
        public TimeSpan                 StartTime { get; set; }
        public TimeSpan?                EndTime { get; set; }
        public string                   Description { get; set; }
        public long?                    TaskID { get; set; }
        public TaskAPI                  Task { get; set; }
        public TimeLineAPI              TimeLine { get; set; }
        public List<CheckInAPI>         CheckIns { get; set; }
        public List<TaskLogCommentAPI>  Comments { get; set; }

        public static TaskLog To(TaskLogAPI item)
        {
            return new TaskLog
            {
                ID = item.ID,
                LogDate = item.LogDate,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                Description = item.Description,
                TaskID = item.TaskID,
                Task = TaskAPI.To(item.Task, false),
                TimeLine = TimeLineAPI.To(item.TimeLine),
                CheckIns = CheckInAPI.To(item.CheckIns),
                Comments = TaskLogCommentAPI.To(item.Comments)
            };
        }

        public static TaskLogAPI From(TaskLog item)
        {
            return new TaskLogAPI
            {
                ID = item.ID,
                LogDate = item.LogDate,
                StartTime = item.StartTime,
                EndTime = item.EndTime,
                Description = item.Description,
                TaskID = item.TaskID,
                Task = TaskAPI.From(item.Task, false),
                TimeLine = TimeLineAPI.From(item.TimeLine),
                CheckIns = item.CheckIns != null ? CheckInAPI.From(item.CheckIns).ToList() : null,
                Comments = item.Comments != null ? TaskLogCommentAPI.From(item.Comments).ToList() : null
            };
        }

        public static ICollection<TaskLog> To(ICollection<TaskLogAPI> items)
        {
            if (items != null)
            {
                ICollection<TaskLog> list = new List<TaskLog>();
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

        public static ICollection<TaskLogAPI> From(ICollection<TaskLog> items)
        {
            if (items != null)
            {
                ICollection<TaskLogAPI> list = new List<TaskLogAPI>();
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
