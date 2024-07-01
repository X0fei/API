using System.ComponentModel.DataAnnotations.Schema;

namespace FisrtAPI.Models
{
    public class Sensors
    {
        [Column("sensor_id")]
        public int SensorID { get; set; }
        [Column("sensor_name")]
        public string SensorName { get; set; }
    }
}
