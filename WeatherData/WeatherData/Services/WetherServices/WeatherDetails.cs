using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Weather.Utilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace WeatherData.Services.WetherServices
{
    public class WeatherDetails:IWeatherDetails
    {

        private readonly IOptions<WeatherConfiguration> _WeatherConfiguration;

        public WeatherDetails(IOptions<WeatherConfiguration> WeatherConfiguration)
        {
            _WeatherConfiguration = WeatherConfiguration;
        }
        public async Task<dynamic> GetWeatherDetailsAsync(string cityID)
        {
            using (var client = new HttpClient())
            {
                var result = "";
                try
                {
                    client.BaseAddress = new Uri(_WeatherConfiguration.Value.WebUrl);
                    var response = await client.GetAsync($"/data/2.5/weather?id={cityID}&appid=" + _WeatherConfiguration.Value.AppId);
                    response.EnsureSuccessStatusCode();
                    result = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<weather>(result);
                    return new
                    {
                        temperatur = rawWeather.Main.Temp,
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        city = rawWeather.Name
                    };


                }
                catch (Exception ex)
                {
                    throw ex;
                    // return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
                return result;
            }
        }

    }
}
