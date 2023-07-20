using E_Commerce.API.Errors;
using ECommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public BugController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            var something = context.Products.Find(100);
            if (something is null)
            {
                return NotFound(new APIResponse(404));
            }
            return Ok(something);
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var something = context.Products.Find(100);
            var somethingtoreturn = something.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new APIResponse(404));
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }

    }
}
