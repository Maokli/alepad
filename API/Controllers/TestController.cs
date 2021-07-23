using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class TestController: BaseApiController
    {
        [HttpGet]
        public ActionResult Test() {
            return Ok("congratz");
        }
    }
}