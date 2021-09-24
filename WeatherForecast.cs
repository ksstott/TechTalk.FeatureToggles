namespace TechTalk.FeatureToggles
{
    using System;
    using System.Text.Json.Serialization;

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? TemperatureF { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Summary { get; set; }
    }
}
