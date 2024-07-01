using System.ComponentModel.DataAnnotations.Schema;

namespace FisrtAPI.Models
{
    public class Meteostations
    {
        [Column("station_id")]
        public int StationID { get; set; }
        [Column("station_name")]
        public string StationName { get; set; }
        [Column("station_longitude")]
        public int StationLongitude { get; set; }
        [Column("station_latitude")]
        public int StationLatitude { get; set; }
    }
}
