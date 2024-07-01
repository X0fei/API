using System.ComponentModel.DataAnnotations.Schema;

namespace FisrtAPI.Models
{
    public class MeasurementsType
    {
        [Column("type_id")]
        public int TypeID { get; set; }
        [Column("type_name")]
        public string TypeName { get; set; }
        [Column("type_units")]
        public string TypeUnits { get; set; }
    }
}
