using JobLogger.API.Model;
using JobLogger.BF;
using JobLogger.BF.ListModels;
using JobLogger.DAL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JobLogger.API.Controllers
{
    [Route("api/[controller]")]
    public class TaskLogController : ControllerBase
    {
        public TaskLogController([FromServices] JobLoggerDbContext db) : base(db)
        { }

        [HttpGet("{id}")]
        [Route("{id}", Name = "TaskLogRoute")]
        public IActionResult Get(long id)
        {
            TaskLog item = new TaskLogBF(DB).Get(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(TaskLogAPI.From(item));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TaskLogAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            TaskLog TaskLog = TaskLogAPI.To(item);

            try
            {
                TaskLog = new TaskLogBF(DB).Create(TaskLog);

                return CreatedAtRoute(
                    "TaskLogRoute",
                    new
                    {
                        controller = "TaskLog",
                        id = TaskLog.ID
                    },
                    TaskLogAPI.From(TaskLog));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] TaskLogAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            try
            {
                TaskLog updateItem = new TaskLogBF(DB).Update(TaskLogAPI.To(item));

                return new ObjectResult(TaskLogAPI.From(updateItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public TaskLogList Get(int page, int pagesize)
        {
            return new TaskLogBF(DB).List(page, pagesize);
        }
    }
}
