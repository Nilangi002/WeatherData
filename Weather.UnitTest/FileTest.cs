using System;
using System.IO;
using Xunit;

namespace WeatherData.UnitTest
{
    public class FileTest
    {
        string path = "";
        public FileTest()
        {
             path = @"D:\WeatherData\WeatherData\WeatherData\wwwroot\InputFile\Weather.txt";
        }
        [Fact]
        public void isInputFilePresent()
        {
           bool present = File.Exists(path);
            Assert.True(present);
        }
        [Fact]
        public void isInputFileContentPresent()
        {
            bool istextcontain = true;
            string text = File.ReadAllText(path);

            if (text == null) istextcontain = false;
            Assert.True(istextcontain);
        }
    }
}
