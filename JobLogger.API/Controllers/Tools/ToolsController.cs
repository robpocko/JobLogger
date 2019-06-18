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
        public IActionResult BackupDatabase()
        {
            return new ObjectResult("i did this");
        }
    }
}
