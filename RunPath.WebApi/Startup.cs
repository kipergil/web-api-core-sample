using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalErrorHandling.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RunPath.WebApi.Domain;
using RunPath.WebApi.Services;
using RunPath.WebApi.Services.Interfaces;

namespace RunPath.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IHttpDataReaderService<>), typeof(HttpDataReaderService<>));
            services.AddTransient(typeof(IPhotoService), typeof(PhotoService));
            services.AddTransient(typeof(IAlbumService), typeof(AlbumService));
            services.AddTransient(typeof(ILogger), typeof(LoggerFactory));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.ConfigureExceptionHandler(loggerFactory);
            app.UseMvc();
        }
    }
}
