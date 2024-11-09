using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AIRQUALITY.Entites
{
    public class IQAirDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public IQAirDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
        }
        public DbSet<ParisAirQuality> ParisAirQuality { get; set; }

    }
}
