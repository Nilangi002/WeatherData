using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;


namespace WeatherData.Services.FileServices
{
    public interface IFileOperations
    {


        IList<City> ReadFile(string FilePath);
        Task<bool> WriteFile(City city, IHostingEnvironment _hostingEnvironment, dynamic content);

    }
}
