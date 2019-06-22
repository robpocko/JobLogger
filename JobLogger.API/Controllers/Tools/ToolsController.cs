using JobLogger.DAL;
using Microsoft.AspNetCore.Mvc;

namespace JobLogger.API.Controllers.Tools
{
    [Route("api/[controller]")]
    public class ToolsController : ControllerBase
    {
        public ToolsController([FromServices] JobLoggerDbContext db) : base(db)
        { }

        [HttpPost]
        [Route("BackupDatabase")]
        public IActionResult BackupDatabase()
        {
            return new ObjectResult("Doing a database backup");
        }

        [HttpPost]
        [Route("RestoreDatabase")]
        public IActionResult RestoreDatabase()
        {
            return new ObjectResult("Doing a database restore");
        }
    }
}
