using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Domain.Entities
{
    public class Current
    {
        [Display(Description = "Observation Time")]
        [JsonProperty("observation_time")]
        //[HumanReadableName("Observation Time")]
        public string ObservationTime { get; set; }

        [Display(Description = "Temperature")]
        [JsonProperty("temperature")]
        public int Temperature { get; set; }

        [Display(Description = "Weather Code")]
        [JsonProperty("weather_code")]
        public int WeatherCode { get; set; }

        [JsonProperty("weather_icons")]
        [Display(Description = "Weather icon")]
        public List<string> WeatherIcons { get; set; }

        [JsonProperty("weather_descriptions")]
        [Display(Description = "Weather Description")]
        public List<string> WeatherDescriptions { get; set; }

        [JsonProperty("wind_speed")]
        [Display(Description = "Wind Speed")]
        public int WindSpeed { get; set; }

        [JsonProperty("wind_degree")]
        [Display(Description = "Wind Degree")]
        public int WindDegree { get; set; }

        [JsonProperty("wind_dir")]
        [Display(Description = "Wind Direction")]
        public string WindDir { get; set; }

        [JsonProperty("pressure")]
        [Display(Description = "Pressure")]
        public int Pressure { get; set; }

        [JsonProperty("precip")]
        [Display(Description = "Precipitation")]
        public int Precip { get; set; }

        [JsonProperty("humidity")]
        [Display(Description = "Humidity")]
        public int Humidity { get; set; }

        [JsonProperty("cloudcover")]
        [Display(Description = "Cloud Cover")]
        public int Cloudcover { get; set; }

        [JsonProperty("feelslike")]
        [Display(Description = "Feels Like")]
        public int Feelslike { get; set; }

        [JsonProperty("uv_index")]
        [Display(Description = "UV Index")]
        public int UVIndex { get; set; }

        [JsonProperty("visibility")]
        [Display(Description = "Visibility")]
        public int Visibility { get; set; }

        [JsonProperty("is_day")]
        [Display(Description = "Is Day?")]
        public string IsDay { get; set; }
    }

}
