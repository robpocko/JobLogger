using JobLogger.DAL;
using System;

namespace JobLogger.BF
{
    public class TaskCheckInBF
    {
        private JobLoggerDbContext db;

        public TaskCheckInBF(JobLoggerDbContext db)
        {
            this.db = db;
        }

        public TaskCheckIn Create(TaskCheckIn item)
        {
            try
            {
                db.TaskCheckIns.Add(item);
                db.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                db.TaskCheckIns.Remove(item);
                throw ex;
            }
        }
    }
}
