using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RunPath.WebApi.Services;
using RunPath.WebApi.Services.Interfaces;

namespace RunPath.WebApi.Tests
{
    public class DependencyResolver
    {
        public ServiceProvider ServiceProvider { get; set; }
        public readonly IPhotoService PhotoService;
        public readonly IAlbumService AlbumService;
        public readonly IMemoryCache Cache;

        public DependencyResolver()
        {
            var services = new ServiceCollection();

            services.AddTransient(typeof(IHttpDataReaderService<>), typeof(HttpDataReaderService<>));
            services.AddTransient(typeof(IPhotoService), typeof(PhotoService));
            services.AddTransient(typeof(IAlbumService), typeof(AlbumService));
            services.AddTransient(typeof(ILogger), typeof(LoggerFactory));
            services.AddMemoryCache();

            ServiceProvider = services.BuildServiceProvider();

            Cache = ServiceProvider.GetService<IMemoryCache>();
            PhotoService = ServiceProvider.GetService<IPhotoService>();
            AlbumService = ServiceProvider.GetService<IAlbumService>();
        }
    }
}
