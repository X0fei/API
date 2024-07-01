using FisrtAPI.Models;
using FisrtAPI.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Expressions;
using System.Diagnostics;

namespace FisrtAPI.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class SensorMeasurementsController : ControllerBase
    {
        private readonly SensorsContext _context;

        public SensorMeasurementsController(SensorsContext sensorsContext)
        {
            _context = sensorsContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SensorMeasurementsRequest>>> GetSensorsMeasurements()
        {

            var requestSensors = new List<SensorMeasurementsRequest>();
            var measurementsSensorList = await _context.SensorMeasurements.Select(it => it).ToListAsync();

            foreach (var mesurementSensor in measurementsSensorList) {
                var requestSensor = new SensorMeasurementsRequest();
                var sensorResult = await _context.Sensors.Select(it => it).Where(it => it.SensorID == mesurementSensor.sensor_id).FirstAsync();

                requestSensor.SensorID = sensorResult.SensorID;
                requestSensor.SensorName = sensorResult.SensorName;
                requestSensor.Measurements = new List<MeasurementsType>();

                if (requestSensors.Find(it => it.SensorID == requestSensor.SensorID) != null)
                {
                    continue;
                }

                requestSensors.Add(requestSensor);
            }

            foreach (var sensor in requestSensors)
            {
                var mesurement = measurementsSensorList.Select(it => it).Where(it => it.sensor_id == sensor.SensorID);
                foreach (var item in mesurement)
                {
                    var mesurResult = await _context.MesaurementsType.Select(it => it).Where(it => it.TypeID == item.type_id).FirstAsync();
                    sensor.Measurements.Add(mesurResult);
                }
            }

            return requestSensors;
        }

        [HttpPost]
        public async Task<ActionResult<SensorMeasurements>> AddSensorMeasurements(int id, int[] addID)
        {
            var requestSensors = new List<SensorMeasurementsRequest>();

            foreach (var sensor in addID)
            {
                SensorMeasurements sensorMeasurements = new SensorMeasurements();
                sensorMeasurements.sensor_id = id;
                sensorMeasurements.type_id = sensor;
                _context.SensorMeasurements.Add(sensorMeasurements);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<SensorMeasurements>> RemoveSensorMeasurements(int id, int [] removeID)
        {
            var requestSensors = new List<SensorMeasurementsRequest>();
            var measurementsSensorList = await _context.SensorMeasurements.Select(it => it).Where(it => it.sensor_id == id).ToListAsync();

            foreach (var sensor in measurementsSensorList) {
                if (removeID.Contains(sensor.type_id)) { 
                    _context.SensorMeasurements.Remove(sensor);
                    await _context.SaveChangesAsync();
                }
            }
            
            return NoContent();
        }
    }
}
