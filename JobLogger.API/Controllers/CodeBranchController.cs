using JobLogger.API.Model;
using JobLogger.BF;
using JobLogger.BF.ListModels;
using JobLogger.DAL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JobLogger.API.Controllers
{
    [Route("api/[controller]")]
    public class CodeBranchController : ControllerBase
    {
        public CodeBranchController([FromServices] JobLoggerDbContext db) : base(db)
        { }

        [HttpGet("{id}")]
        [Route("{id}", Name = "CodeBranchRoute")]
        public IActionResult Get(long id)
        {
            CodeBranch item = new CodeBranchBF(DB).Get(id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(CodeBranchAPI.From(item));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CodeBranchAPI item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            CodeBranch CodeBranch = CodeBranchAPI.To(item);

            try
            {
                CodeBranch = new CodeBranchBF(DB).Create(CodeBranch);

                return CreatedAtRoute(
                    "CodeBranchRoute",
                    new
                    {
                        controller = "CodeBranch",
                        id = CodeBranch.ID
                    },
                    CodeBranchAPI.From(CodeBranch));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public CodeBranchList Get(int page, int pagesize)
        {
            return new CodeBranchBF(DB).List(page, pagesize);
        }
    }
}
