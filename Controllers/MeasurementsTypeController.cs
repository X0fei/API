using FisrtAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FisrtAPI.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class MeasurementsTypeController : ControllerBase
    {
        private readonly SensorsContext _context;

        public MeasurementsTypeController(SensorsContext sensorsContext)
        {
            _context = sensorsContext;        
        }

        [HttpGet]
        public async Task<ActionResult<List<MeasurementsType>>> GetMeasurementsTypes() 
        {
            return await _context.MesaurementsType.Select(measurementsType => measurementsType).ToListAsync();
        }

        [HttpGet(template: "{id}")]
        public async Task<ActionResult<MeasurementsType>> GetMeasurementsTypesItem(int id)
        {
            return await _context
                .MesaurementsType
                .FindAsync(id) ?? throw new InvalidOperationException();
        }
        
        [HttpPost]
        public async Task<ActionResult<MeasurementsType>> AddMeasurementsType(MeasurementsType measurementsType)
        {
            _context.Add(measurementsType);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<MeasurementsType>> RemoveMeasurementsType(int id)
        {
            MeasurementsType? measurementsType = await _context.MesaurementsType.FindAsync(id);
            if (measurementsType != null)
            {
                _context.MesaurementsType.Remove(measurementsType);
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }

        [HttpPut(template: "{id}")]
        public async Task<ActionResult<MeasurementsType>> UpdateMeasurementsType(int id, MeasurementsType measurementsTypeRequest)
        {
            MeasurementsType? measurementsType = await _context.MesaurementsType.FindAsync(id);
            if (measurementsType != null)
            {
                measurementsType.TypeName = measurementsTypeRequest.TypeName;
                measurementsType.TypeUnits = measurementsTypeRequest.TypeUnits;
                await _context.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
