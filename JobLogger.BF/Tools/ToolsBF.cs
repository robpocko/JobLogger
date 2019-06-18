using JobLogger.BF.ListModels;
using JobLogger.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobLogger.BF.Tools
{
    public class ToolsBF
    {
        private JobLoggerDbContext db;

        public ToolsBF(JobLoggerDbContext db)
        {
            this.db = db;
        }

        public bool BackupDatabase()
        { 
            return true;
        }
    }
}
