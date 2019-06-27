using JobLogger.BF.ListModels;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JobLogger.BF
{
    public class FeatureBF
    {
        private JobLoggerDbContext db;

        public FeatureBF(JobLoggerDbContext db)
        {
            this.db = db;
        }

        public Feature Create(Feature item)
        {
            if (item.IsValid())
            {
                try
                {
                    db.Features.Add(item);
                    db.SaveChanges();

                    MarkNotIsNew(item);
                    return item;
                }
                catch (Exception ex)
                {
                    db.Features.Remove(item);
                    throw ex;
                }
            }
            else
            {
                throw new Exception("Feature is invalid");
            }
        }

        public Feature Update(Feature item)
        {
            Feature updated = FetchAndUpdate(item);
            
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
                throw new Exception("Feature is invalid");
            }
        }

        public Feature Get(long id)
        {
            try
            {
                return db.Features
                            .Where(i => i.ID == id)
                            .Include(feat => feat.Requirements).ThenInclude(req => req.Comments)
                            .Include(feat => feat.Requirements).ThenInclude(req => req.Tasks).ThenInclude(task => task.Comments)
                            .Include(feat => feat.Requirements).ThenInclude(req => req.Tasks).ThenInclude(task => task.CheckIns)
                            .Include(feat => feat.Requirements).ThenInclude(req => req.Tasks).ThenInclude(task => task.Logs).ThenInclude(log => log.Comments)
                            .Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FeatureList List(int page, int pagesize, string title = "", RequirementStatus? status = null)
        {
            try
            {
                IQueryable<FeatureListModel> features =
                    from feature in db.Features
                    where (title == "" || feature.Title.ToLower().Contains(title.ToLower())) &&
                          (!status.HasValue || feature.Status == status.Value)
                    select new FeatureListModel { ID = feature.ID, Title = feature.Title, Status = feature.Status };

                int recCount = features.Count();
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
                    var results = features.OrderByDescending(c => c.ID)
                                    .Skip(page * pagesize)
                                    .Take(fetchCount)
                                    .ToList();
                    return new FeatureList { RecordCount = recCount, Data = results };
                }
                else
                {
                    return new FeatureList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Feature FetchAndUpdate(Feature item)
        {
            Feature fetched = Get(item.ID);

            fetched.Title = item.Title;
            fetched.Status = item.Status;




            foreach (var requirement in fetched.Requirements)
            {
                //requirement.Title =
                //comment.Comment = item.Comments.Where(c => c.ID == comment.ID).Single().Comment;
            }

            foreach (var requirement in item.Requirements)
            {
                if (requirement.IsNew)
                {
                    requirement.IsNew = false;
                    fetched.Requirements.Add(requirement);
                }
            }



            return fetched;
        }

        internal static void MarkNotIsNew(Feature item)
        {
            item.IsNew = false;
            if (item.Requirements != null)
            {
                foreach (var requirement in item.Requirements)
                {
                    RequirementBF.MarkNotIsNew(requirement);
                }
            }
        }
    }
}
