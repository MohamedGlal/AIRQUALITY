using AIRQUALITY.HostedJobs;
using AIRQUALITY.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace AIRQUALITY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirQualityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IQAirService _airService;
        private CRONJOB _cronJob;
        public AirQualityController(IConfiguration configuration, IQAirService airService)
        {
            _configuration = configuration;
            _airService = airService;
        }
        [HttpGet("nearest-city")]
        public async Task<IActionResult> Get([FromQuery] double latitude, [FromQuery] double longitude) 
        {
            var result = await _airService.GetNearestCityAirQuality(latitude, longitude);
            return result != null ?Ok(result) : NotFound();
        }
        [HttpGet("most-polluted-time")]
        public async Task<IActionResult> GetMostTime()
        {
            var result = await _airService.GetMostPollutedTime();
            return result != null ? Ok(result) : NotFound();
        }
    }
}
