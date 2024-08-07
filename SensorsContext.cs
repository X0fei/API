﻿using Microsoft.EntityFrameworkCore;
using FisrtAPI.Models;

namespace FisrtAPI;

public class SensorsContext : DbContext
{
    public SensorsContext(DbContextOptions<SensorsContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sensors>().ToTable("sensors").HasKey(sensor => sensor.SensorID);

        modelBuilder.Entity<MeasurementsType>().ToTable("measurements_type").HasKey(measurementsType => measurementsType.TypeID);

        modelBuilder.Entity<Meteostations>().ToTable("meteostations").HasKey(meteostations => meteostations.StationID);
        modelBuilder.Entity<SensorMeasurements>().ToTable("sensors_measurements").HasKey(sensorMeasurements => new { sensorMeasurements.sensor_id, sensorMeasurements.type_id });

    }

    public DbSet<Sensors> Sensors { get; set; } = null!;
    public DbSet<MeasurementsType> MesaurementsType { get; set; } = null!;
    public DbSet<Meteostations> Meteostations { get; set; } = null!;
    public DbSet<SensorMeasurements> SensorMeasurements { get; set; } = null!;
}
