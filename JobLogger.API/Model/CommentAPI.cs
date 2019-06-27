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

    public class RequirementCommentAPI : APIBase
    {
        public string Comment { get; set; }
        public long RequirementID { get; set; }
        public RequirementAPI Requirement { get; set; }

        public static RequirementComment To(RequirementCommentAPI item)
        {
            return new RequirementComment
            {
                ID = item.ID,
                Comment = item.Comment,
                RequirementID = item.RequirementID
            };
        }

        public static RequirementCommentAPI From(RequirementComment item)
        {
            return new RequirementCommentAPI
            {
                ID = item.ID,
                Comment = item.Comment,
                RequirementID = item.RequirementID
            };
        }

        public static ICollection<RequirementComment> To(ICollection<RequirementCommentAPI> items)
        {
            if (items != null)
            {
                ICollection<RequirementComment> list = new List<RequirementComment>();
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

        public static ICollection<RequirementCommentAPI> From(ICollection<RequirementComment> items)
        {
            if (items != null)
            {
                ICollection<RequirementCommentAPI> list = new List<RequirementCommentAPI>();
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

    public class TaskLogCommentAPI : APIBase
    {
        public string Comment { get; set; }
        public long TaskLogID { get; set; }
        public TaskLogAPI Task { get; set; }

        public static TaskLogComment To(TaskLogCommentAPI item)
        {
            return new TaskLogComment
            {
                ID = item.ID,
                Comment = item.Comment,
                TaskLogID = item.TaskLogID
            };
        }

        public static TaskLogCommentAPI From(TaskLogComment item)
        {
            return new TaskLogCommentAPI
            {
                ID = item.ID,
                Comment = item.Comment,
                TaskLogID = item.TaskLogID
            };
        }

        public static ICollection<TaskLogComment> To(ICollection<TaskLogCommentAPI> items)
        {
            if (items != null)
            {
                ICollection<TaskLogComment> list = new List<TaskLogComment>();
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

        public static ICollection<TaskLogCommentAPI> From(ICollection<TaskLogComment> items)
        {
            if (items != null)
            {
                ICollection<TaskLogCommentAPI> list = new List<TaskLogCommentAPI>();
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
