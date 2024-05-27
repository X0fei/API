using Microsoft.EntityFrameworkCore;
using FisrtAPI.Models;

namespace FisrtAPI;

public class SensorsContext : DbContext
{
    public SensorsContext(DbContextOptions<SensorsContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sensor>().ToTable("sensor").HasKey(sensor => sensor.sensor_id);
    }

    public DbSet<Sensor> Sensors { get; set; } = null!;
}
