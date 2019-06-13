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
    public class RequirementController : ControllerBase
    {
        public RequirementController([FromServices] JobLoggerDbContext db) : base(db)
        { }

        [HttpGet("{id}")]
        [Route("{id}", Name = "RequirementRoute")]
        public IActionResult Get(long id)
        {
            Requirement item = new RequirementBF(DB).Get(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(RequirementAPI.From(item));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] RequirementAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            Requirement Requirement = RequirementAPI.To(item);

            try
            {
                Requirement = new RequirementBF(DB).Create(Requirement);

                return CreatedAtRoute(
                    "RequirementRoute",
                    new
                    {
                        controller = "Requirement",
                        id = Requirement.ID
                    },
                    RequirementAPI.From(Requirement));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] RequirementAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            try
            {
                Requirement updateItem = new RequirementBF(DB).Update(RequirementAPI.To(item));

                return new ObjectResult(RequirementAPI.From(updateItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public RequirementList Get(int page, int pagesize, string title = "", RequirementStatus? status = null)
        {
            return new RequirementBF(DB).List(page, pagesize, title, status);
        }
    }
}
