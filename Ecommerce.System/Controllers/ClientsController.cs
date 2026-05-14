using Microsoft.AspNetCore.Mvc;
using Ecommerce.System.Core.Models;
using Ecommerce.System.Infrastructure.Persistence.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Clients
        [HttpPost]
        public async Task<IActionResult> CreateClient(Client client)
        {
            // Zakładam, że w AppDbContext masz teraz: public DbSet<Client> Clients { get; set; }
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return Ok(client);
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }
    }
}