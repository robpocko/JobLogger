using JobLogger.BF.ListModels;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JobLogger.BF
{
    public class RequirementBF
    {
        private JobLoggerDbContext db;

        public RequirementBF(JobLoggerDbContext db)
        {
            this.db = db;
        }

        public Requirement Create(Requirement item)
        {
            if (item.IsValid())
            {
                try
                {
                    db.Requirements.Add(item);
                    db.SaveChanges();

                    MarkNotIsNew(item);
                    return item;
                }
                catch (Exception ex)
                {
                    db.Requirements.Remove(item);
                    throw ex;
                }
            }
            else
            {
                throw new Exception("Requirement is invalid");
            }
        }

        public Requirement Update(Requirement item)
        {
            Requirement updated = FetchAndUpdate(item);

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

        public Requirement Get(long id)
        {
            try
            {
                return db.Requirements
                            .Where(i => i.ID == id)
                            .Include(requirement => requirement.Comments)
                            .Include(requirement => requirement.Tasks).ThenInclude(task => task.Comments)
                            .Include(requirement => requirement.Tasks).ThenInclude(task => task.CheckIns)
                            .Include(requirement => requirement.Tasks).ThenInclude(task => task.Logs).ThenInclude(log => log.Comments)
                            .Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RequirementList List(int page, int pagesize, string title = "", RequirementStatus? status = null)
        {
            try
            {
                IQueryable<RequirementListModel> requirements =
                    from requirement in db.Requirements
                    where (title == "" || requirement.Title.ToLower().Contains(title.ToLower())) &&
                          (!status.HasValue || requirement.Status == status.Value)
                    select new RequirementListModel { ID = requirement.ID, Title = requirement.Title, Status = requirement.Status };

                int recCount = requirements.Count();
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
                    var results = requirements.OrderByDescending(c => c.ID)
                                    .Skip(page * pagesize)
                                    .Take(fetchCount)
                                    .ToList();
                    return new RequirementList { RecordCount = recCount, Data = results };
                }
                else
                {
                    return new RequirementList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal Requirement FetchAndUpdate(Requirement item)
        {
            Requirement fetched = Get(item.ID);

            fetched.Title = item.Title;
            fetched.Status = item.Status;

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

            if (item.Tasks != null)
            {
                TaskBF bf = new TaskBF(db);

                foreach (var task in item.Tasks)
                {
                    if (task.IsNew)
                    {
                        fetched.Tasks.Add(task);
                    }
                    else
                    {
                        var temp = fetched.Tasks.Where(t => t.ID == task.ID).Single();
                        temp = bf.FetchAndUpdate(task);
                    }
                }
            }

            MarkNotIsNew(fetched);
            return fetched;
        }

        internal static void MarkNotIsNew(Requirement item)
        {
            item.IsNew = false;
            if (item.Tasks != null)
            {
                foreach (var task in item.Tasks)
                {
                    TaskBF.MarkNotIsNew(task);
                }
            }
        }
    }
}
