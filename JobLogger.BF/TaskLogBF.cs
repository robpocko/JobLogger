using JobLogger.BF.ListModels;
using JobLogger.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JobLogger.BF
{
    public class TaskLogBF
    {
        private JobLoggerDbContext db;

        public TaskLogBF(JobLoggerDbContext db)
        {
            this.db = db;
        }

        public TaskLog Create(TaskLog item)
        {
            if (item.IsValid())
            {
                try
                {
                    db.TaskLogs.Add(item);
                    db.SaveChanges();

                    if (item.TaskID.HasValue)
                    {
                        item.Task = db.Tasks.Where(t => t.ID == item.TaskID.Value).Single();
                    }

                    MarkNotIsNew(item);
                    return item;
                }
                catch (Exception ex)
                {
                    db.TaskLogs.Remove(item);
                    throw ex;
                }
            }
            else
            {
                throw new Exception("TaskLog is invalid");
            }
        }

        public TaskLog Update(TaskLog item)
        {
            TaskLog updated = FetchAndUpdate(item);

            if (updated.IsValid())
            {
                try
                {
                    db.SaveChanges();
                    return updated;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("Requirement is invalid");
            }
        }

        public TaskLog Get(long id)
        {
            try
            {
                return db.TaskLogs
                            .Where(i => i.ID == id)
                            .Include(taskLog => taskLog.Task)
                            .Include(taskLog => taskLog.CheckIns)
                            .Include(taskLog => taskLog.Comments)
                            .Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TaskLogList List(int page, int pagesize)
        {
            try
            {
                IQueryable<TaskLogListModel> tasklogs =
                    from taskLog in db.TaskLogs
                    select new TaskLogListModel { ID = taskLog.ID, Description = taskLog.Description, LogDate = taskLog.LogDate.Date };

                int recCount = tasklogs.Count();
                int fetchCount = pagesize;
                if (pagesize == 0)
                {
                    fetchCount = recCount;
                }
                else if ((page + 1) * pagesize > recCount)
                {
                    fetchCount = recCount - page * pagesize;
                }

                if (fetchCount > 0)
                {
                    var results = tasklogs.OrderByDescending(c => c.ID)
                                    .Skip(page * pagesize)
                                    .Take(fetchCount)
                                    .ToList();
                    return new TaskLogList { RecordCount = recCount, Data = results };
                }
                else
                {
                    return new TaskLogList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal TaskLog FetchAndUpdate(TaskLog item)
        {
            TaskLog fetched = Get(item.ID);

            fetched.Description = item.Description;
            fetched.EndTime = item.EndTime;
            fetched.LogDate = item.LogDate;
            fetched.StartTime = item.StartTime;

            if (item.CheckIns != null)
            {
                CheckInBF bf = new CheckInBF(db);
                foreach (var checkIn in item.CheckIns)
                {
                    if (checkIn.IsNew)
                    {
                        fetched.CheckIns.Add(checkIn);
                    }
                    else
                    {
                        var temp = fetched.CheckIns.Where(l => l.ID == checkIn.ID).Single();
                        temp = bf.FetchAndUpdate(checkIn);
                    }
                }
            }

            if (item.Comments != null)
            {
                foreach (var comment in item.Comments)
                {
                    if (comment.ID <= 0)
                    {
                        fetched.Comments.Add(comment);
                    }
                    else
                    {
                        fetched.Comments.Where(c => c.ID == comment.ID).Single().Comment = comment.Comment;
                    }
                }
            }

            MarkNotIsNew(fetched);
            return fetched;
        }

        internal static void MarkNotIsNew(TaskLog item)
        {
            if (item.CheckIns != null)
            {
                foreach (var checkIn in item.CheckIns)
                {
                    CheckInBF.MarkNotIsNew(checkIn);
                }
            }
        }
    }
}
