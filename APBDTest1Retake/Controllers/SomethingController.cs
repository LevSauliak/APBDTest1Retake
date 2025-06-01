using APBDTest1Retake.Repositories;
using APBDTest1Retake.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBDTest1Retake.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomethingController : ControllerBase
    {
        private ISomethingService _somethingService;

        public SomethingController(ISomethingService somethingService)
        {
            _somethingService = somethingService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }
        
    }
}
