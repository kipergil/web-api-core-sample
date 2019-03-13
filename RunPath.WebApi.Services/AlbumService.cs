using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using RunPath.WebApi.Domain;
using RunPath.WebApi.Services.Interfaces;

namespace RunPath.WebApi.Services
{
    public class AlbumService : IAlbumService
    {
        public string ApiUrl { get; set; } = "http://jsonplaceholder.typicode.com/albums";

        private readonly IHttpDataReaderService<Album> _dataReaderService;
        private readonly IMemoryCache _cache;

        public AlbumService(IHttpDataReaderService<Album> dataReaderService, IMemoryCache cache)
        {
            _dataReaderService = dataReaderService;
            _cache = cache;
        }

        public List<Album> GetAllByUserId(int userId)
        {
            var result = GetAll().Where(i => i.userId.Equals(userId)).ToList();
            return result;
        }

        public List<Album> GetAll()
        {
            var cackeKey = CacheHelper.GetCacheKey("albums");

            var data = _cache.GetOrCreate(cackeKey, entry =>
            {
                entry.SlidingExpiration = CacheHelper.GetCacheExpiration();
                return _dataReaderService.ReadAsList(ApiUrl);
            });


            return data;

        }

        public Album GetById(int id)
        {
            return GetAll().FirstOrDefault(i => i.id.Equals(id));
        }

    }
}
