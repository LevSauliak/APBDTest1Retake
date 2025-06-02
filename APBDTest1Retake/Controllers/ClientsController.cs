using APBDTest1Retake.Exceptions;
using APBDTest1Retake.Models.DTOs;
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
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _clientsService.GetClient(id);
            if (client == null)
            {
                return NotFound("No client with this id found");
            }
            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> AddClientWithRental([FromBody] ClientWithRentalPostDto dto)
        {
            try
            {
                var id = await _clientsService.AddClientWithRental(dto);
                return Created($"api/clients/{id}", id);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error");
            }
            
        }
        
    }
}
