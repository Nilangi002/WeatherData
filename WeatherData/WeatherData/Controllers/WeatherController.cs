using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Weather.Utilities;
using WeatherData.Services.FileServices;
using WeatherData.Services.WetherServices;

namespace WeatherData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFileOperations _file;
        private readonly IWeatherDetails _weather;
        public WeatherController(IHostingEnvironment hostingEnvironment, IFileOperations file, IWeatherDetails weather)
        {
            _hostingEnvironment = hostingEnvironment;
            _file = file;
            _weather = weather;
        }
        [HttpGet]
        public async Task<bool> Get()
        {
            bool isfilecreated = true;
            string path = @_hostingEnvironment.WebRootPath + "\\InputFile\\Weather.txt";
            //string path = @"D:\\WeatherData\\WeatherData\\WeatherData\\wwwroot\\InputFile\\weather.txt";
            try
            {
                IList<City> cities = _file.ReadFile(path);

                foreach (City c in cities)
                {

                    dynamic res = await _weather.GetWeatherDetailsAsync(c.CityId);
                    isfilecreated = await _file.WriteFile(c, _hostingEnvironment, res);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return isfilecreated;
        }
    }
}
