﻿using JobLogger.BF.ListModels;
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
                            .Include(r => r.Requirements)
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
            Feature fetched = db.Features
                                .Where(i => i.ID == item.ID)
                                .Single();

            fetched.Title = item.Title;
            fetched.Status = item.Status;

            return fetched;
        }
    }
}
