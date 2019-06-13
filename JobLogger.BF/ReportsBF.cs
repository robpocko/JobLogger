using JobLogger.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobLogger.BF
{
    public class ReportsBF
    {
        private JobLoggerDbContext db;

        public ReportsBF(JobLoggerDbContext db)
        {
            this.db = db;
        }


    }
}
