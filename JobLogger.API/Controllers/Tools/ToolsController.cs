using JobLogger.API.Model;
using JobLogger.BF;
using JobLogger.BF.ListModels;
using JobLogger.DAL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JobLogger.API.Controllers.Tools
{
    [Route("api/[controller]")]
    public class ToolsController : ControllerBase
    {
        public ToolsController([FromServices] JobLoggerDbContext db) : base(db)
        { }

        [HttpPost]
        public IActionResult BackupDatabase()
        {
            return new ObjectResult("i did this");
        }
    }
}
