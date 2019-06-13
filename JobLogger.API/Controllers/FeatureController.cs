using Microsoft.AspNetCore.Mvc;
using JobLogger.DAL;
using JobLogger.BF;
using JobLogger.API.Model;
using System;
using System.Collections.Generic;
using JobLogger.BF.ListModels;
using JobLogger.DAL.Common;

namespace JobLogger.API.Controllers
{
    [Route("api/[controller]")]
    public class FeatureController : ControllerBase
    {
        public FeatureController([FromServices] JobLoggerDbContext db) : base(db)
        { }

        [HttpGet("{id}")]
        [Route("{id}", Name = "FeatureRoute")]
        public IActionResult Get(long id)
        {
            Feature item = new FeatureBF(DB).Get(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(FeatureAPI.From(item));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] FeatureAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            Feature Feature = FeatureAPI.To(item);

            try
            {
                Feature = new FeatureBF(DB).Create(Feature);

                return CreatedAtRoute(
                    "FeatureRoute",
                    new
                    {
                        controller = "Feature",
                        id = Feature.ID
                    },
                    FeatureAPI.From(Feature));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] FeatureAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            try
            {
                Feature updateItem = new FeatureBF(DB).Update(FeatureAPI.To(item));

                return new ObjectResult(FeatureAPI.From(updateItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public FeatureList Get(int page, int pagesize, string title = "", RequirementStatus? status = null)
        {
            return new FeatureBF(DB).List(page, pagesize, title, status);
        }
    }
}
