using JobLogger.API.Model;
using JobLogger.BF;
using JobLogger.BF.ListModels;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JobLogger.API.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        public TaskController([FromServices] JobLoggerDbContext db) : base(db)
        { }

        [HttpGet("{id}")]
        [Route("{id}", Name = "TaskRoute")]
        public IActionResult Get(long id)
        {
            Task item = new TaskBF(DB).Get(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(TaskAPI.From(item));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TaskAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            Task task = TaskAPI.To(item);

            try
            {
                task = new TaskBF(DB).Create(task);

                return CreatedAtRoute(
                    "TaskRoute",
                    new
                    {
                        controller = "Task",
                        id = task.ID
                    },
                    TaskAPI.From(task));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] TaskAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            try
            {
                Task updateItem = new TaskBF(DB).Update(TaskAPI.To(item));

                return new ObjectResult(TaskAPI.From(updateItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public TaskList Get(int page, int pagesize, string title = "", TaskType? taskType = null, bool showInActive = false)
        {
            return new TaskBF(DB).List(page, pagesize, showInActive, title, taskType);
        }
    }
}
