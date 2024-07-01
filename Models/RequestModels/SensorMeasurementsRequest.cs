namespace FisrtAPI.Models.RequestModels
{
    public class SensorMeasurementsRequest
    {
        public int SensorID { get; set; }
        public string SensorName { get; set; }
        public List<MeasurementsType> Measurements { get; set; }
        public string MeasurementFormula { get; set; }
    }
}
