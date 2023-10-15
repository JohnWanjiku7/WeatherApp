namespace WeatherApp.Domain.Entities
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public ErrorDetails Error { get; set; }
    }
   
}
