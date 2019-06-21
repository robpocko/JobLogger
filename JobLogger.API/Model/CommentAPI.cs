using JobLogger.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobLogger.API.Model
{
    public class TaskCommentAPI : APIBase
    {
        public string Comment { get; set; }
        public long TaskID { get; set; }
        public TaskAPI Task { get; set; }

        public static TaskComment To(TaskCommentAPI item)
        {
            return new TaskComment
            {
                ID = item.ID,
                Comment = item.Comment,
                TaskID = item.TaskID
            };
        }

        public static TaskCommentAPI From(TaskComment item)
        {
            return new TaskCommentAPI
            {
                ID = item.ID,
                Comment = item.Comment,
                TaskID = item.TaskID
            };
        }

        public static ICollection<TaskComment> To(ICollection<TaskCommentAPI> items)
        {
            if (items != null)
            {
                ICollection<TaskComment> list = new List<TaskComment>();
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

        public static ICollection<TaskCommentAPI> From(ICollection<TaskComment> items)
        {
            if (items != null)
            {
                ICollection<TaskCommentAPI> list = new List<TaskCommentAPI>();
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
