using JobLogger.DAL;
using JobLogger.DAL.Common;
using System.Collections.Generic;

namespace JobLogger.API.Model
{
    public class TaskAPI : APIBase
    {
        public string Title { get; set; }
        public TaskType TaskType { get; set; }
        public bool IsActive { get; set; }
        public long? RequirementID { get; set; }
        public RequirementAPI Requirement { get; set; }
        public ICollection<TaskCheckInAPI> CheckIns { get; set; }
        public ICollection<TaskLogAPI> Logs { get; set; }


        public static Task To(TaskAPI item, bool includeTaskLogs = true)
        {
            if (item == null)
            {
                return null;
            }
            else
            {
                return new Task
                {
                    ID = item.ID,
                    Title = item.Title,
                    TaskType = item.TaskType,
                    IsActive = item.IsActive,
                    CheckIns = TaskCheckInAPI.To(item.CheckIns),
                    Logs = includeTaskLogs ? TaskLogAPI.To(item.Logs) : null,
                    RequirementID = item.RequirementID,
                    Requirement = RequirementAPI.To(item.Requirement),
                    IsNew = item.IsNew
                };
            }

        }

        public static TaskAPI From(Task item, bool includeTaskLogs = true)
        {
            if (item == null)
            {
                return null;
            }
            else
            {
                return new TaskAPI
                {
                    ID = item.ID,
                    Title = item.Title,
                    TaskType = item.TaskType,
                    IsActive = item.IsActive,
                    CheckIns = TaskCheckInAPI.From(item.CheckIns),
                    Logs = includeTaskLogs ? TaskLogAPI.From(item.Logs) : null,
                    RequirementID = item.RequirementID,
                    Requirement = RequirementAPI.From(item.Requirement, false),
                    IsNew = item.IsNew
                };
            }

        }

        public static ICollection<Task> To(ICollection<TaskAPI> items)
        {
            if (items != null)
            {
                ICollection<Task> list = new List<Task>();
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

        public static ICollection<TaskAPI> From(ICollection<Task> items)
        {
            if (items != null)
            {
                ICollection<TaskAPI> list = new List<TaskAPI>();
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
