using Microsoft.EntityFrameworkCore;
using FisrtAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FisrtAPI.Controllers;

[Route(template:"api/[controller]")]
[ApiController]
public class SensorController : ControllerBase
{
    private readonly SensorsContext _sensorsContext;

    public SensorController(SensorsContext context)
    {
        _sensorsContext = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Sensor>>> GetSensors()
    {
        return await _sensorsContext.Sensors.Select(sensor => sensor).ToListAsync();
    }

    [HttpGet(template: "{id}")]
    public async Task<ActionResult<Sensor>> GetSensorItem(int id)
    {
        return await _sensorsContext
            .Sensors
            .FindAsync(id) ?? throw new InvalidOperationException();
    }

    [HttpPost]
    public async Task<ActionResult<Sensor>> AddSensor(Sensor sensor)
    {
        _sensorsContext.Add(sensor);
        await _sensorsContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete(template:"{id}")]
    public async Task<ActionResult<Sensor>> RemoveSensor(int id)
    {
        Sensor? sensor = await _sensorsContext.Sensors.FindAsync(id);
        if (sensor != null)
        {
            _sensorsContext.Sensors.Remove(sensor);
            await _sensorsContext.SaveChangesAsync();
        }
        return NoContent();
    }

    [HttpPut(template:"{id}")]
    public async Task<ActionResult<Sensor>> UpdateSensor(int id, Sensor sensorRequest)
    {
        Sensor? sensor = await _sensorsContext.Sensors.FindAsync(id);
        if (sensor != null)
        {
            sensor.sensor_name = sensorRequest.sensor_name;
            await _sensorsContext.SaveChangesAsync();
        }
        return NoContent();
    }
}
