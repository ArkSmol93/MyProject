using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResourcesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/resources
        [HttpGet]
        public async Task<IActionResult> GetResources()
        {
            var resources = await _context.Resources.ToListAsync();
            return Ok(resources);
        }

        // POST: api/resources
        [HttpPost]
        public async Task<IActionResult> CreateResource(Resource resource)
        {
            resource.LastUpdated = DateTime.UtcNow; // ustawienie daty tworzenia
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            // Wysyłka wiadomości do RabbitMQ
            SendRabbitMessage($"Zasób {resource.Id} został dodany");

            return CreatedAtAction(nameof(GetResources), new { id = resource.Id }, resource);
        }

        // PUT: api/resources/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(int id, Resource resource)
        {
            var existing = await _context.Resources.FindAsync(id);
            if (existing == null) return NotFound();

            // Sprawdzenie konfliktu
            if (existing.LastUpdated > resource.LastUpdated)
            {
                return Conflict("Dane zostały zmienione przez innego użytkownika");
            }

            // Aktualizacja
            existing.Name = resource.Name;
            existing.Description = resource.Description;
            existing.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // Wysyłka wiadomości do RabbitMQ
            SendRabbitMessage($"Zasób {existing.Id} został zaktualizowany");

            return NoContent();
        }

        // DELETE: api/resources/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var existing = await _context.Resources.FindAsync(id);
            if (existing == null) return NotFound();

            _context.Resources.Remove(existing);
            await _context.SaveChangesAsync();

            // Wysyłka wiadomości do RabbitMQ
            SendRabbitMessage($"Zasób {id} został usunięty");

            return NoContent();
        }

        // ---- Pomocnicza metoda do wysyłania wiadomości do RabbitMQ ----
        private void SendRabbitMessage(string message)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "syncQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: "syncQueue",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine("Wysłano wiadomość: " + message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd wysyłki RabbitMQ: " + ex.Message);
            }
        }
    }
}
