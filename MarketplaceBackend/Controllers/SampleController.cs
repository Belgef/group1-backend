using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult Hello()
        {
            return Ok(new {message="Hello, " + User.Claims.First(e=>e.Type=="fname") });
        }
    }
}
