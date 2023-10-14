namespace WeatherApp.Domain.Entities
{
    public class RequestCS
    {
        public string type { get; set; }
        public string query { get; set; }
        public string language { get; set; }
        public string unit { get; set; }
    }
}
