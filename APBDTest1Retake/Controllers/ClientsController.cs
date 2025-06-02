using APBDTest1Retake.Repositories;
using APBDTest1Retake.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBDTest1Retake.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetClient(int id)
        {
            var client = await _clientsService.GetClient(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
        
    }
}
