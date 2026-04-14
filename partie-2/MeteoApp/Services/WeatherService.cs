using System.Text.Json;
using MeteoApp.Models;

namespace MeteoApp.Services
{
    public class WeatherService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BaseUrl = "https://api.weatherapi.com/v1";

        public WeatherService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<WeatherResponse?> GetForecastAsync(string city, int days = 3)
        {
            try
            {
                string url = $"{BaseUrl}/forecast.json?key={_apiKey}&q={Uri.EscapeDataString(city)}&days={days}&lang=fr&aqi=no";
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                string json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<WeatherResponse>(json);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Image?> GetWeatherIconAsync(string iconUrl)
        {
            try
            {
                if (iconUrl.StartsWith("//"))
                    iconUrl = "https:" + iconUrl;

                byte[] imageBytes = await _httpClient.GetByteArrayAsync(iconUrl);
                using MemoryStream ms = new(imageBytes);
                return Image.FromStream(ms);
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
