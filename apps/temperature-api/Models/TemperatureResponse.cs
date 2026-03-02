namespace TemperatureApi.Models;

public class TemperatureResponse
{
    public double Value { get; set; }
    public string Unit { get; set; } = "celsius";
    public DateTime Timestamp { get; set; }
    public string Location { get; set; } = "";
    public string Status { get; set; } = "ok";
    public string SensorId { get; set; } = "";
    public string SensorType { get; set; } = "";
    public string Description { get; set; } = "";
}