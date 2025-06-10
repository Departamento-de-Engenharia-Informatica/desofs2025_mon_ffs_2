using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AMAPP.API.Controllers
{
    [ApiController]                      
    [Route("health")]                    
    public class HealthController : ControllerBase
    {

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok(new { status = "Healthy" });
    }
}
