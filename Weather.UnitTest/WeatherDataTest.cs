
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
  public  class WeatherDataTest
    {
        string cityID = "";

        string Appid = "aa69195559bd4f88d79f9aadeb77a8f6";
        string appurl = "https://api.openweathermap.org";
        public WeatherDataTest()
        {
            cityID = "2988507";
           
        }
        [Fact]
        public async void isWeatherDeatilsPresentAsync()
        {
            using (var client = new HttpClient())
            {
                var result = "";

                client.BaseAddress = new Uri(appurl);
                var response = await client.GetAsync($"/data/2.5/weather?id={cityID}&appid=" + Appid);
                response.EnsureSuccessStatusCode(); 

                if (response.IsSuccessStatusCode)
                    Assert.True(true);
            }
        }


        [Fact]
        public async void isWeatherDeatilsNotPresentAsync()
        {
            cityID = "2988507ff";
            using (var client = new HttpClient())
            {
                var result = "";

                client.BaseAddress = new Uri(appurl);
                var response = await client.GetAsync($"/data/2.5/weather?id={cityID}&appid=" + Appid);
                //response.EnsureSuccessStatusCode(); 

                if (!response.IsSuccessStatusCode)
                    Assert.True(true);

            }
        }

        
    }
}
