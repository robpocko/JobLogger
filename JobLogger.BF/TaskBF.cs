using JobLogger.BF.ListModels;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JobLogger.BF
{
    public class TaskBF
    {
        private JobLoggerDbContext db;

        public TaskBF(JobLoggerDbContext db)
        {
            this.db = db;
        }

        public Task Create(Task item)
        {
            if (item.IsValid())
            {
                try
                {
                    db.Tasks.Add(item);
                    db.SaveChanges();

                    return item;
                }
                catch (Exception ex)
                {
                    db.Tasks.Remove(item);
                    throw ex;
                }
            }
            else
            {
                throw new Exception("Task is invalid");
            }
        }

        public Task Update(Task item)
        {
            Task updated = FetchAndUpdate(item);

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
                throw new Exception("Task is invalid");
            }
        }

        public Task Get(long id)
        {
            try
            {
                return db.Tasks
                            .Where(i => i.ID == id)
                            .Include(c => c.CheckIns)
                            .Include(l => l.Logs)
                            .Include(r => r.Requirement)
                            .Include(cc => cc.Comments)
                            .Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TaskList List(int page, int pagesize, bool showInActive, string title = "", TaskType? taskType = null)
        {
            try
            {
                IQueryable<TaskListModel> tasks =
                    from task in db.Tasks
                    where (title == "" || task.Title.ToLower().Contains(title.ToLower())) &&
                          (!taskType.HasValue || task.TaskType == taskType.Value) &&
                          (showInActive || task.IsActive)   
                    select new TaskListModel { ID = task.ID, Title = task.Title, TaskType = task.TaskType };

                int recCount = tasks.Count();
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
                    var results = tasks.OrderByDescending(c => c.ID)
                                    .Skip(page * pagesize)
                                    .Take(fetchCount)
                                    .ToList();
                    return new TaskList { RecordCount = recCount, Data = results };
                }
                else
                {
                    return new TaskList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Task FetchAndUpdate(Task item)
        {
            Task fetched = db.Tasks
                                .Where(i => i.ID == item.ID)
                                .Single();

            fetched.Title = item.Title;
            fetched.IsActive = item.IsActive;
            fetched.TaskType = item.TaskType;

            return fetched;
        }
    }
}
