using JobLogger.API.Model;
using JobLogger.BF;
using JobLogger.BF.ListModels;
using JobLogger.DAL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JobLogger.API.Controllers
{
    [Route("api/[controller]")]
    public class CheckInController : ControllerBase
    {
        public CheckInController([FromServices] JobLoggerDbContext db) : base(db)
        { }

        [HttpGet("{id}")]
        [Route("{id}", Name = "CheckInRoute")]
        public IActionResult Get(long id)
        {
            CheckIn item = new CheckInBF(DB).Get(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(CheckInAPI.From(item));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CheckInAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            CheckIn checkIn = CheckInAPI.To(item);


            try
            {
                checkIn = new CheckInBF(DB).Create(checkIn);

                return CreatedAtRoute(
                    "CheckInRoute",
                    new
                    {
                        controller = "CheckIn",
                        id = checkIn.ID
                    },
                    CheckInAPI.From(checkIn));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public CheckInList Get(int page, int pagesize, string comment = "", long? codeBranchID = null)
        {
            return new CheckInBF(DB).List(page, pagesize, comment, codeBranchID);
        }
    }
}
