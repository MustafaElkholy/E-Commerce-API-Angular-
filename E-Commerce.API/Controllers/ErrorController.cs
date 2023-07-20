using E_Commerce.API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ErrorController : ControllerBase
    {

        public IActionResult Error(int code)
        {
            return new ObjectResult(new APIResponse(code));
        }
    }
}
