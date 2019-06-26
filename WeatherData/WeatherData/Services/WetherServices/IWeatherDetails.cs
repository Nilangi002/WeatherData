using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherData.Services.WetherServices
{
    public interface IWeatherDetails
    {

        Task<dynamic> GetWeatherDetailsAsync(string cityID);

    }
}
