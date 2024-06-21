using Microsoft.AspNetCore.Mvc;

namespace Diversos.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Init()
        {
            return "Manguera Car - API en linea";
        }
    }
}
