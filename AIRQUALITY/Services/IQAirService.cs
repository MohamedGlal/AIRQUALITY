using AIRQUALITY.Entites;
using AIRQUALITY.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace AIRQUALITY.Services
{
    public class IQAirService
    {
        private readonly IConfiguration _configuration;
        private IQAirDbContext _dbContext;
        public IQAirService(IConfiguration configuration, IQAirDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }
        public async Task<IQAirResponse> GetNearestCityAirQuality (double latitude, double longitude)
        {
            using (HttpClient client = new HttpClient())
            {
                var responseTask =  client.GetAsync(_configuration["API_SETTINGS:API_URL"]+$"?key={_configuration["API_SETTINGS:API_KEY"]}&lon={longitude}&lat={latitude}");
                responseTask.Wait();
                if (responseTask.IsCompleted)
                {
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var messageTask = result.Content.ReadAsStringAsync();
                        messageTask.Wait();
                        IQAirResponse response = JsonSerializer.Deserialize<IQAirResponse>(messageTask.Result);
                        return response;
                    }
                }
                return new IQAirResponse();
            }
        }
        public async Task<DateTime?> GetMostPollutedTime()
        {
            var mostTime = await _dbContext.ParisAirQuality.OrderByDescending(s => s.aqicn).FirstOrDefaultAsync();
            if (mostTime != null) return mostTime.Created;
            return null;
        }
    }
}
