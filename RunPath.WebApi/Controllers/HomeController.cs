using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RunPath.WebApi.Domain;
using RunPath.WebApi.Services;
using RunPath.WebApi.Services.Interfaces;

namespace RunPath.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IPhotoService _photoService;
        private readonly IAlbumService _albumService;
        private readonly IMemoryCache _cache;

        public HomeController(IPhotoService photoService, IAlbumService albumService, IMemoryCache cache)
        {
            _photoService = photoService;
            _albumService = albumService;
            _cache = cache;
        }

        [HttpGet]
        [Route("exception")]
        public ActionResult Exception()
        {
            throw new Exception("Example exception");
        }

        [HttpGet]
        [Route("photos")]
        public ActionResult<List<Photo>> GetPhotos()
        {

            var result = _photoService.GetAll();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("photos/{id}")]
        public ActionResult<Photo> GetPhotosById(int id)
        {
            var result = _photoService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("album/photos/{albumId}")]
        public ActionResult<List<Photo>> GetPhotosByAlbumId(int albumId)
        {
            var result = _photoService.GetAllByAlbumId(albumId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("albums")]
        public ActionResult<List<Album>> GetAlbums()
        {
            var result = _albumService.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("albums/{id}")]
        public ActionResult<Album> GetAlbumsById(int id)
        {
            var result = _albumService.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("albums/user/{userId}")]
        public ActionResult<List<Album>> GetAlbumsByUserId(int userId)
        {
            var result = _albumService.GetAllByUserId(userId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("albums/{albumId}/photos")]
        public ActionResult<List<Album>> GetAlbumsWithPhotosByAlbumId(int albumId)
        {
            var result = _albumService.GetById(albumId);

            if (result == null)
            {
                return NotFound();
            }

            result.albumPhotos = _photoService.GetAllByAlbumId(albumId);

            return Ok(result);
        }

        [HttpGet]
        [Route("user/{userId}/albums")]
        public ActionResult<List<Album>> GetAlbumsWithPhotosByUserId(int userId, bool loadPhotos = false)
        {
            var result = _albumService.GetAllByUserId(userId);

            if (result == null)
            {
                return NotFound();
            }

            foreach (var album in result)
            {
                if (loadPhotos)
                {
                    album.albumPhotos = _photoService.GetAllByAlbumId(album.id);
                }
                else
                {
                    album.albumPhotos = new List<Photo>();
                }
            }

            return Ok(result);
        }

    }
}
