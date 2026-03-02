using Microsoft.AspNetCore.Mvc;
using TemperatureApi.Models;

namespace TemperatureApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TemperatureController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromQuery] string location)
    {
        var response = new TemperatureResponse
        {
            Value = Random.Shared.Next(-30, 40),
            Unit = "celsius",
            Timestamp = DateTime.UtcNow,
            Location = location,
            Status = "ok",
            SensorId = "sensor-1",
            SensorType = "virtual",
            Description = $"Temperature in {location}"
        };

        return Ok(response);
    }

    [HttpGet("{sensorId}")]
    public IActionResult GetById(string sensorId)
    {
        var response = new TemperatureResponse
        {
            Value = Random.Shared.Next(-30, 40),
            Unit = "celsius",
            Timestamp = DateTime.UtcNow,
            Location = "unknown",
            Status = "ok",
            SensorId = sensorId,
            SensorType = "virtual",
            Description = $"Sensor {sensorId}"
        };

        return Ok(response);
    }
}