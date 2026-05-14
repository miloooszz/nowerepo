using Ecommerce.System.Core.Interfaces;
using Ecommerce.System.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Client client)
        {
            await _clientRepository.AddAsync(client);
            return Ok(client);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientRepository.GetAllAsync();
            return Ok(clients);
        }
    }
}