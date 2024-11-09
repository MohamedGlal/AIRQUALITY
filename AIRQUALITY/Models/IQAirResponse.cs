namespace AIRQUALITY.Models
{
    public class IQAirResponse
    {
        public string status { get; set; }
        public IQAirData? data { get; set; }

    }
    public class IQAirData
    {
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public Location? location { get; set; }
        public Current? current { get; set; }
    }
}
