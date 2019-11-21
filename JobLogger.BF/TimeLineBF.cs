using JobLogger.BF.ListModels;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JobLogger.BF
{
    public class TimeLineBF
    {
        private JobLoggerDbContext db;

        public TimeLineBF(JobLoggerDbContext db)
        {
            this.db = db;
        }

        public TimeLine Create(TimeLine item)
        {
            if (item.IsValid())
            {
                try
                {
                    db.TimeLines.Add(item);
                    db.SaveChanges();

                    MarkNotIsNew(item);
                    return item;
                }
                catch (Exception ex)
                {
                    db.TimeLines.Remove(item);
                    throw ex;
                }
            }
            else
            {
                throw new Exception("TimeLine is invalid");
            }
        }

        public TimeLine Update(TimeLine item)
        {
            TimeLine updated = FetchAndUpdate(item);

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
                throw new Exception("TimeLine is invalid");
            }
        }

        public TimeLine Get(long id)
        {
            try
            {
                return db.TimeLines
                            .Where(i => i.ID == id)
                            .Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TimeLineList List(int page, int pagesize, string title = "", bool isActiveOnly = false)
        {
            try
            {
                IQueryable<TimeLineListModel> timeLines =
                    from timeline in db.TimeLines
                    where (title == "" || timeline.Title.ToLower().Contains(title.ToLower())) &&
                          (!isActiveOnly || timeline.IsActive)
                    select new TimeLineListModel { ID = timeline.ID, Title = timeline.Title, IsActive = timeline.IsActive };

                int recCount = timeLines.Count();
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
                    var results = timeLines.OrderByDescending(c => c.ID)
                                    .Skip(page * pagesize)
                                    .Take(fetchCount)
                                    .ToList();
                    return new TimeLineList { RecordCount = recCount, Data = results };
                }
                else
                {
                    return new TimeLineList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal TimeLine FetchAndUpdate(TimeLine item)
        {
            TimeLine fetched = Get(item.ID);

            fetched.Title = item.Title;
            fetched.IsActive = item.IsActive;

            MarkNotIsNew(fetched);
            return fetched;
        }

        internal static void MarkNotIsNew(TimeLine item)
        {
            item.IsNew = false;
        }
    }
}
