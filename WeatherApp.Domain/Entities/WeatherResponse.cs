namespace WeatherApp.Domain.Entities
{
    public class WeatherResponse
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
        public Request Request { get; set; }
    }
}
