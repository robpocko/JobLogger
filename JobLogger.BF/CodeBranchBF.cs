﻿using JobLogger.BF.ListModels;
using JobLogger.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobLogger.BF
{
    public class CodeBranchBF
    {
        private JobLoggerDbContext db;

        public CodeBranchBF(JobLoggerDbContext db)
        {
            this.db = db;
        }

        public CodeBranch Create(CodeBranch item)
        {
            if (item.IsValid())
            {
                try
                {
                    db.CodeBranches.Add(item);
                    db.SaveChanges();

                    return item;
                }
                catch (Exception ex)
                {
                    db.CodeBranches.Remove(item);
                    throw ex;
                }
            }
            else
            {
                throw new Exception("CodeBranch is invalid");
            }
        }

        public CodeBranch Update(CodeBranch item)
        {
            CodeBranch updated = FetchAndUpdate(item);

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
                throw new Exception("CodeBranch is invalid");
            }
        }

        public CodeBranch Get(long id)
        {
            try
            {
                return db.CodeBranches
                            .Where(i => i.ID == id)
                            .Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CodeBranchList List(int page, int pagesize, string name = "", bool includeInActive = false)
        {
            try
            {
                IQueryable<CodeBranchListModel> codeBranches =
                    from codebranch in db.CodeBranches
                    where (name == "" || codebranch.Name.ToLower().Contains(name.ToLower())) &&  
                          (includeInActive || codebranch.IsActive)
                    select new CodeBranchListModel { ID = codebranch.ID, Name = codebranch.Name };

                int recCount = codeBranches.Count();
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
                    var results = codeBranches.OrderBy(c => c.Name)
                                    .Skip(page * pagesize)
                                    .Take(fetchCount)
                                    .ToList();
                    return new CodeBranchList { RecordCount = recCount, Data = results };
                }
                else
                {
                    return new CodeBranchList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal CodeBranch FetchAndUpdate(CodeBranch item)
        {
            CodeBranch fetched = Get(item.ID);

            fetched.Name = item.Name;
            fetched.IsActive = item.IsActive;

            return fetched;
        }
    }
}
