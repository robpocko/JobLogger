using JobLogger.BF.ReportBF;
using JobLogger.BF.ReportModels;
using JobLogger.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace JobLogger.API.Controllers.Reports
{
    [Route("api/[controller]")]
    public class TimesheetReportsController : ControllerBase
    {
        public TimesheetReportsController([FromServices] JobLoggerDbContext db) : base(db)
        { }

        [HttpGet("{reportDate}")]
        [Route("TimesheetForDay")]
        public List<TimesheetReportModel> TimesheetForDay(DateTime reportDate)
        {
            var items = new TimesheetReportBF(DB).GetTimesheetForDay(reportDate);

            return items;
        }
    }
}
