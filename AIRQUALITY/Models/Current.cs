namespace AIRQUALITY.Models
{
    public class Current
    {
        public Pollution pollution { get; set; }
        public Weather weather { get; set; }
    }
    public class Pollution
    {
        public DateTime? ts { get; set; }
        public decimal? aqius { get; set; }
        public string? mainus { get; set; }
        public decimal? aqicn { get; set; }
        public string? maincn { get; set; }

    }
    public class Weather
    {
        public DateTime? ts { get; set; }
        public decimal? tp { get; set; }
        public decimal? pr { get; set; }
        public decimal? hu { get; set; }
        public decimal? ws { get; set; }
        public decimal? wd { get; set; }
        public string? ic { get; set; }

    }
}
