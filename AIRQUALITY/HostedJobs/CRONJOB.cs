
using AIRQUALITY.Entites;
using AIRQUALITY.Services;
using Microsoft.EntityFrameworkCore;

namespace AIRQUALITY.HostedJobs
{
    public class CRONJOB : BackgroundService
    {
        private IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;
        public CRONJOB(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await AddParisAirQuality();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        private async Task AddParisAirQuality()
        {
            // GET Paris Air Quality data by calling the api
            using var qAirServiceScope = _serviceScopeFactory.CreateScope();
            var _qAirService = qAirServiceScope.ServiceProvider.GetRequiredService<IQAirService>();
            var result =await _qAirService.GetNearestCityAirQuality(double.Parse(_configuration["API_SETTINGS:PARIS_LAT"]), double.Parse(_configuration["API_SETTINGS:PARIS_LON"]));
            // storing the data into the db
            if (result != null) 
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<IQAirDbContext>();
                dbContext.ParisAirQuality.Add(new ParisAirQuality
                {
                    Created = DateTime.UtcNow,
                    aqicn = result.data.current.pollution.aqicn,
                    aqius = result.data.current.pollution.aqius,
                    maincn = result.data.current.pollution.maincn,
                    ts = result.data.current.pollution.ts,
                    mainus = result.data.current.pollution.mainus
                });
                await dbContext.SaveChangesAsync();
            }
        }
        
    }
}
