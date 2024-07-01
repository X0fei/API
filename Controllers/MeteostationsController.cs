using FisrtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FisrtAPI.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class MeteostationsController : ControllerBase
    {
        private readonly SensorsContext _context;

        public MeteostationsController(SensorsContext sensorsContext)
        {
            _context = sensorsContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Meteostations>>> GetMeteostations()
        {
            return await _context.Meteostations.Select(meteostations => meteostations).ToListAsync();
        }

        [HttpGet(template: "{id}")]
        public async Task<ActionResult<Meteostations>> GetMeteostationsItem(int id)
        {
            return await _context
                .Meteostations
                .FindAsync(id) ?? throw new InvalidOperationException();
        }

        [HttpPost]
        public async Task<ActionResult<Meteostations>> AddMeteostation(Meteostations meteostations)
        {
            _context.Add(meteostations);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<Meteostations>> RemoveMeteostation(int id)
        {
            Meteostations? meteostations = await _context.Meteostations.FindAsync(id);
            if (meteostations != null)
            {
                _context.Meteostations.Remove(meteostations);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

        [HttpPut(template: "{id}")]
        public async Task<ActionResult<Meteostations>> UpdateMeteostation(int id, Meteostations meteostationsRequest)
        {
            Meteostations? meteostations = await _context.Meteostations.FindAsync(id);
            if (meteostations != null)
            {
                meteostations.StationName = meteostationsRequest.StationName;
                meteostations.StationLongitude = meteostationsRequest.StationLongitude;
                meteostations.StationLatitude = meteostationsRequest.StationLatitude;
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
