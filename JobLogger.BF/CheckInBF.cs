using JobLogger.BF.ListModels;
using JobLogger.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobLogger.BF
{
    public class CheckInBF
    {
        private JobLoggerDbContext db;

        public CheckInBF(JobLoggerDbContext db)
        {
            this.db = db;
        }

        public CheckIn Create(CheckIn item)
        {
            if (item.IsValid())
            {
                //  to make sure that we don't re-insert the task
                if (item.TaskLog != null)
                {
                    long taskLogId = (item.TaskLog != null) ? item.TaskLog.ID : item.TaskLogID.Value;
                    item.TaskLog = db.TaskLogs.Where(t => t.ID == taskLogId).Single();

                    if (item.TaskCheckIns == null || item.TaskCheckIns.Count == 0)
                    {
                        item.TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = item.TaskLog.TaskID.Value } };
                    }
                }

                try
                {
                    db.CheckIns.Add(item);
                    db.SaveChanges();

                    return item;
                }
                catch (Exception ex)
                {
                    db.CheckIns.Remove(item);
                    throw ex;
                }
            }
            else
            {
                throw new Exception("CheckIn is invalid");
            }
        }

        public CheckIn Get(long id)
        {
            try
            {
                return db.CheckIns
                            .Where(i => i.ID == id)
                            .Include(t => t.TaskLog)
                            //.Include(b => b.CodeBranch)
                            .Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CheckInList List(int page, int pagesize, string comment = "", long? codeBranchID = null)
        {
            try
            {
                IQueryable<CheckInListModel> checkins =
                    from checkin in db.CheckIns
                    where (comment == "" || checkin.Comment.ToLower().Contains(comment.ToLower())) &&
                            (!codeBranchID.HasValue || checkin.CodeBranchID == codeBranchID.Value)
                    select new CheckInListModel { ID = checkin.ID, Comment = checkin.Comment };

                int recCount = checkins.Count();
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
                    var results = checkins.OrderByDescending(c => c.ID)
                                    .Skip(page * pagesize)
                                    .Take(fetchCount)
                                    .ToList();
                    return new CheckInList { RecordCount = recCount, Data = results };
                }
                else
                {
                    return new CheckInList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
