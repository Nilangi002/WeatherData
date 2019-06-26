using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Weather.Utilities;
using WeatherData.Services.FileServices;
using WeatherData.Services.WetherServices;

namespace WeatherData
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            #region "read configurations"

            services.Configure<WeatherConfiguration>(options =>
            {
                options.AppId = this.Configuration.GetSection("WeatherConfiguration").GetValue<string>("AppId");
                options.WebUrl = this.Configuration.GetSection("WeatherConfiguration").GetValue<string>("WebUrl");
                options.InputFilepath = this.Configuration.GetSection("WeatherConfiguration").GetValue<string>("InputFilepath");
            });
            #endregion

            #region "Dependancy Injection"
            services.AddScoped<IWeatherDetails, WeatherDetails>();
            services.AddScoped<IFileOperations, FileOperations>();


            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
