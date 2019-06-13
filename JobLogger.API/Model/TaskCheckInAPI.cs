using JobLogger.DAL;
using System.Collections.Generic;

namespace JobLogger.API.Model
{
    public class TaskCheckInAPI
    {
        public long TaskID { get; set; }
        public long CheckInID { get; set; }

        public static TaskCheckIn To(TaskCheckInAPI item)
        {
            return new TaskCheckIn
            {
                TaskID = item.TaskID,
                CheckInID = item.CheckInID
            };
        }

        public static TaskCheckInAPI From(TaskCheckIn item)
        {
            return new TaskCheckInAPI
            {
                TaskID = item.TaskID,
                CheckInID = item.CheckInID
            };
        }

        public static ICollection<TaskCheckIn> To(ICollection<TaskCheckInAPI> items)
        {
            if (items != null)
            {
                ICollection<TaskCheckIn> list = new List<TaskCheckIn>();
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

        public static ICollection<TaskCheckInAPI> From(ICollection<TaskCheckIn> items)
        {
            if (items != null)
            {
                ICollection<TaskCheckInAPI> list = new List<TaskCheckInAPI>();
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
