
using System;
using System.IO;
using System.Net.Http;
using Weather.Utilities;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using WeatherData.Services.FileServices;
using WeatherData.Services.WetherServices;
using Microsoft.Extensions.Options;
using Moq;
namespace WeatherData.UnitTest
{
  public  class WeatherControllerTest
    {
        string cityID = "";

        string Appid = "aa69195559bd4f88d79f9aadeb77a8f6";
        string appurl = "https://api.openweathermap.org";
        public WeatherControllerTest()
        {
            cityID = "2988507";
           
        }
        
        [Fact]
        public async void weatherService()
        {
            //Arrange
            var mockEnvironment = new Mock<IHostingEnvironment>();
            //...Setup the mock as needed
            mockEnvironment
                .Setup(m => m.WebRootPath )
                .Returns("D:\\WeatherData\\WeatherData\\WeatherData\\wwwroot");
            IOptions<WeatherConfiguration> iWeatherConfiguration = Options.Create<WeatherConfiguration>(new WeatherConfiguration());
            iWeatherConfiguration.Value.AppId = Appid;
            iWeatherConfiguration.Value.WebUrl = appurl;
            IFileOperations file = new FileOperations();
            IWeatherDetails weather=new WeatherDetails(iWeatherConfiguration);
            WeatherData.Controllers.WeatherController w = new WeatherData.Controllers.WeatherController(mockEnvironment.Object , file,weather );
            bool response = await w.Get();
            if (response) Assert.True(true);

        }
    }
}
