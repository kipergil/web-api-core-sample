using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using RunPath.WebApi.Domain;
using RunPath.WebApi.Services.Interfaces;

namespace RunPath.WebApi.Services
{
    public class PhotoService : IPhotoService
    {
        public string ApiUrl { get; set; } = "http://jsonplaceholder.typicode.com/photos";

        private readonly IHttpDataReaderService<Photo> _dataReaderService;
        private readonly IMemoryCache _cache;

        public PhotoService(IHttpDataReaderService<Photo> dataReaderService, IMemoryCache cache)
        {
            _dataReaderService = dataReaderService;
            _cache = cache;
        }

        public List<Photo> GetAll()
        {
            var cackeKey = CacheHelper.GetCacheKey("photos");

            var data = _cache.GetOrCreate(cackeKey, entry =>
            {
                entry.SlidingExpiration = CacheHelper.GetCacheExpiration();
                return _dataReaderService.ReadAsList(ApiUrl);
            });


            return data;
        }

        public List<Photo> GetAllByAlbumId(int albumId)
        {
            var result = GetAll().Where(i => i.albumId.Equals(albumId)).ToList();
            return result;
        }


        public Photo GetById(int id)
        {
            return GetAll().FirstOrDefault(i => i.id.Equals(id));
        }
    }
}