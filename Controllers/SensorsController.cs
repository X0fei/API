using FisrtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FisrtAPI.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class SensorsController : ControllerBase
    {
        private readonly SensorsContext _context;

        public SensorsController(SensorsContext sensorsContext)
        {
            _context = sensorsContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sensors>>> GetSensors()
        {
            return await _context.Sensors.Select(sensor => sensor).ToListAsync();
        }

        [HttpGet(template: "{id}")]
        public async Task<ActionResult<Sensors>> GetSensorsItem(int id)
        {
            return await _context
                .Sensors
                .FindAsync(id) ?? throw new InvalidOperationException();
        }

        [HttpPost]
        public async Task<ActionResult<Sensors>> AddSensor(Sensors sensor)
        {
            _context.Add(sensor);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<Sensors>> RemoveSensor(int id)
        {
            Sensors? sensor = await _context.Sensors.FindAsync(id);
            if (sensor != null)
            {
                _context.Sensors.Remove(sensor);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

        [HttpPut(template: "{id}")]
        public async Task<ActionResult<Sensors>> UpdateSensor(int id, Sensors sensorRequest)
        {
            Sensors? sensor = await _context.Sensors.FindAsync(id);
            if (sensor != null)
            {
                sensor.SensorName = sensorRequest.SensorName;
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
