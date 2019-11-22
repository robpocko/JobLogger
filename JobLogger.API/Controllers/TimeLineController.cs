using JobLogger.API.Model;
using JobLogger.BF;
using JobLogger.BF.ListModels;
using JobLogger.DAL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JobLogger.API.Controllers
{
    [Route("api/[controller]")]
    public class TimeLineController : ControllerBase
    {
        public TimeLineController([FromServices] JobLoggerDbContext db) : base(db)
        { }

        [HttpGet("{id}")]
        [Route("{id}", Name = "TimeLineRoute")]
        public IActionResult Get(long id)
        {
            TimeLine item = new TimeLineBF(DB).Get(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(TimeLineAPI.From(item));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TimeLineAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            TimeLine TimeLine = TimeLineAPI.To(item);

            try
            {
                TimeLine = new TimeLineBF(DB).Create(TimeLine);

                return CreatedAtRoute(
                    "TimeLineRoute",
                    new
                    {
                        controller = "TimeLine",
                        id = TimeLine.ID
                    },
                    TimeLineAPI.From(TimeLine));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] TimeLineAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            try
            {
                TimeLine updateItem = new TimeLineBF(DB).Update(TimeLineAPI.To(item));

                return new ObjectResult(TimeLineAPI.From(updateItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public TimeLineList Get(int page, int pagesize)
        {
            return new TimeLineBF(DB).List(page, pagesize);
        }
    }
}
