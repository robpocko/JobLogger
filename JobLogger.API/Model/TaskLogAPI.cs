using JobLogger.DAL;
using System;
using System.Collections.Generic;

namespace JobLogger.API.Model
{
    public class TaskLogAPI : APIBase
    {
        public DateTime LogDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string Description { get; set; }
        public long? TaskID { get; set; }
        public TaskAPI Task { get; set; }
        public ICollection<CheckInAPI> CheckIns { get; set; }

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
                CheckIns = CheckInAPI.To(item.CheckIns)
            };
        }

        public static TaskLogAPI From(TaskLog item, bool loadCheckins = false)
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
                CheckIns = loadCheckins ? CheckInAPI.From(item.CheckIns) : null
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
