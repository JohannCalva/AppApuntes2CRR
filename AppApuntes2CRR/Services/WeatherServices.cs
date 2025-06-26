using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppApuntes2CRR.Models;
using Newtonsoft.Json;

namespace AppApuntes2CRR.Services
{
    public class WeatherServices
    {
        public async Task<WeatherData> GetWeatherDataFromLocationAsync(double latitude, double longitude)
        {
            string latitude_str = latitude.ToString().Replace(",", ".");
            string longitude_str = longitude.ToString().Replace(",", ".");

            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude_str}&longitude={longitude_str}&current=temperature_2m,relative_humidity_2m,rain&timezone=auto";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                WeatherData data = JsonConvert.DeserializeObject<WeatherData>(result);
                return data;
            }
        }
        public async Task<WeatherData> GetCurrentLocationWeatherAsync()
        {
            GeolocationServices geolocationServices = new GeolocationServices();
            Location location = await geolocationServices.GetCurrentLocation();
            return await GetWeatherDataFromLocationAsync(location.Latitude, location.Longitude);
        }
    }
}
